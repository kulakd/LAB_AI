import numpy as np

def RBF_kernel(XN, XM, KH = 1):
    """
    Inputs:
        xn: row n of x
        xm: row m of x
        l:  kernel hyperparameter, set to 1 by default
    Outputs:
        K:  kernel matrix element: K[n, m] = k(xn, xm)
    """
    KMX = np.exp(-np.linalg.norm(XN - XM)**2 / (2 * KH**2))
    return KMX

def make_RBF_kernel(XR, KH = 1, XN = 0):
    """
    Inputs:
        X: set of φ rows of inputs
        l: kernel hyperparameter, set to 1 by default
        sigma: Gaussian noise std dev, set to 0 by default
    Outputs:
        K:  Covariance matrix 
    """
    CM = np.zeros([len(XR), len(XR)])
    for i in range(len(XR)):
        for j in range(len(XR)):
            CM[i, j] = RBF_kernel(XR[i], XR[j], KH)
    return CM + XN * np.eye(len(CM))

def gaussian_process_predict_mean(X, Y, XN):
    """
    Inputs:
        X: set of φ rows of inputs
        y: set of φ observations 
        X_new: new input 
    Outputs:
        y_new: predicted target corresponding to X_new
    """
    rbf_kernel = make_RBF_kernel(np.vstack([X, XN]))
    K = rbf_kernel[:len(X), :len(X)]
    k = rbf_kernel[:len(X), -1]
    return  np.dot(np.dot(k, np.linalg.inv(K)), Y)

def gaussian_process_predict_std(X, XN):
    """
    Inputs:
        X: set of φ rows of inputs
        X_new: new input
    Outputs:
        y_std: std dev. corresponding to X_new
    """
    rbf_kernel = make_RBF_kernel(np.vstack([X, XN]))
    K = rbf_kernel[:len(X), :len(X)]
    k = rbf_kernel[:len(X), -1]
    return rbf_kernel[-1,-1] - np.dot(np.dot(k,np.linalg.inv(K)),k)

import pandas as pd
import matplotlib.pyplot as plt

data = pd.read_csv('ai_dane.csv')
data = data[data['countriesAndTerritories'] == 'Poland']
data = data[['dateRep','cases']]
data['dateRep'] = pd.to_datetime(data['dateRep'], dayfirst=True)
data = data.sort_values(by='dateRep')

days = np.linspace(1, 365, 729)
trainingDays = 50
X = np.arange(0, trainingDays)
X = X.reshape(-1, 1)
y_pred = []


for i in range(len(days)):
    X_new = np.array([days[i]])
    y_pred.append(gaussian_process_predict_mean(X, data['cases'][:trainingDays], X_new))
y_pred = np.array(y_pred)


plt.figure(figsize = (15, 5))
plt.plot(X, data['cases'][:trainingDays], "ro")
plt.plot(np.arange(trainingDays, data.shape[0]), data['cases'][trainingDays:], "yo")
plt.plot(days, y_pred, "b-")
plt.xlabel("$Day$", fontsize = 14)
plt.ylabel("$Cases$", fontsize = 14)
plt.legend(["Observations in training set", "Real observations", "Predictions"], fontsize = 10)
plt.grid(True)
plt.xticks(fontsize = 10)
plt.yticks(fontsize = 10)
plt.show()
