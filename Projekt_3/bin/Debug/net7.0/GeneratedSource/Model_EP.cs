// <auto-generated />
#pragma warning disable 1570, 1591

using System;
using Microsoft.ML.Probabilistic;
using Microsoft.ML.Probabilistic.Distributions;
using Microsoft.ML.Probabilistic.Factors;

namespace Models
{
	/// <summary>
	/// Generated algorithm for performing inference.
	/// </summary>
	/// <remarks>
	/// If you wish to use this class directly, you must perform the following steps:
	/// 1) Create an instance of the class.
	/// 2) Set the value of any externally-set fields e.g. data, priors.
	/// 3) Call the Execute(numberOfIterations) method.
	/// 4) Use the XXXMarginal() methods to retrieve posterior marginals for different variables.
	/// 
	/// Generated by Infer.NET 0.4.2301.301 at 17:42 on poniedziałek, 5 czerwca 2023.
	/// </remarks>
	public partial class Model_EP : IGeneratedAlgorithm
	{
		#region Fields
		/// <summary>True if Changed_vbool__0 has executed. Set this to false to force re-execution of Changed_vbool__0</summary>
		public bool Changed_vbool__0_isDone;
		/// <summary>Field backing the NumberOfIterationsDone property</summary>
		private int numberOfIterationsDone;
		/// <summary>Field backing the vbool__0 property</summary>
		private bool[] vbool__0_field;
		/// <summary>Message to marginal of 'vbool__0'</summary>
		public DistributionStructArray<Bernoulli,bool> vbool__0_marginal_F;
		/// <summary>Message to marginal of 'vdouble0'</summary>
		public Beta vdouble0_marginal_F;
		#endregion

		#region Properties
		/// <summary>The number of iterations done from the initial state</summary>
		public int NumberOfIterationsDone
		{
			get {
				return this.numberOfIterationsDone;
			}
		}

		/// <summary>The externally-specified value of 'vbool__0'</summary>
		public bool[] vbool__0
		{
			get {
				return this.vbool__0_field;
			}
			set {
				if ((value!=null)&&(value.Length!=100)) {
					throw new ArgumentException(((("Provided array of length "+value.Length)+" when length ")+100)+" was expected for variable \'vbool__0\'");
				}
				this.vbool__0_field = value;
				this.numberOfIterationsDone = 0;
				this.Changed_vbool__0_isDone = false;
			}
		}

		#endregion

		#region Methods
		/// <summary>Computations that depend on the observed value of vbool__0</summary>
		private void Changed_vbool__0()
		{
			if (this.Changed_vbool__0_isDone) {
				return ;
			}
			Beta vBeta0 = Beta.Uniform();
			this.vdouble0_marginal_F = Beta.Uniform();
			Beta[] vdouble0_rep_B;
			// Create array for 'vdouble0_rep' Backwards messages.
			vdouble0_rep_B = new Beta[100];
			for(int index0 = 0; index0<100; index0++) {
				vdouble0_rep_B[index0] = Beta.Uniform();
				// Message to 'vdouble0_rep' from Bernoulli factor
				vdouble0_rep_B[index0] = BernoulliFromBetaOp.ProbTrueAverageConditional(this.vbool__0[index0]);
			}
			Beta vdouble0_rep_B_toDef;
			vdouble0_rep_B_toDef = ReplicateOp_Divide.ToDefInit<Beta>(vBeta0);
			vdouble0_rep_B_toDef = ReplicateOp_Divide.ToDef<Beta>(vdouble0_rep_B, vdouble0_rep_B_toDef);
			// Message to 'vdouble0_marginal' from Variable factor
			this.vdouble0_marginal_F = VariableOp.MarginalAverageConditional<Beta>(vdouble0_rep_B_toDef, vBeta0, this.vdouble0_marginal_F);
			// Create array for 'vbool__0_marginal' Forwards messages.
			this.vbool__0_marginal_F = new DistributionStructArray<Bernoulli,bool>(100);
			for(int index0 = 0; index0<100; index0++) {
				this.vbool__0_marginal_F[index0] = Bernoulli.Uniform();
			}
			// Message to 'vbool__0_marginal' from DerivedVariable factor
			this.vbool__0_marginal_F = DerivedVariableOp.MarginalAverageConditional<DistributionStructArray<Bernoulli,bool>,bool[]>(this.vbool__0, this.vbool__0_marginal_F);
			this.Changed_vbool__0_isDone = true;
		}

		/// <summary>Update all marginals, by iterating message passing the given number of times</summary>
		/// <param name="numberOfIterations">The number of times to iterate each loop</param>
		/// <param name="initialise">If true, messages that initialise loops are reset when observed values change</param>
		private void Execute(int numberOfIterations, bool initialise)
		{
			this.Changed_vbool__0();
			this.numberOfIterationsDone = numberOfIterations;
		}

		/// <summary>Update all marginals, by iterating message-passing the given number of times</summary>
		/// <param name="numberOfIterations">The total number of iterations that should be executed for the current set of observed values.  If this is more than the number already done, only the extra iterations are done.  If this is less than the number already done, message-passing is restarted from the beginning.  Changing the observed values resets the iteration count to 0.</param>
		public void Execute(int numberOfIterations)
		{
			this.Execute(numberOfIterations, true);
		}

		/// <summary>Get the observed value of the specified variable.</summary>
		/// <param name="variableName">Variable name</param>
		public object GetObservedValue(string variableName)
		{
			if (variableName=="vbool__0") {
				return this.vbool__0;
			}
			throw new ArgumentException("Not an observed variable name: "+variableName);
		}

		/// <summary>Get the marginal distribution (computed up to this point) of a variable</summary>
		/// <param name="variableName">Name of the variable in the generated code</param>
		/// <returns>The marginal distribution computed up to this point</returns>
		/// <remarks>Execute, Update, or Reset must be called first to set the value of the marginal.</remarks>
		public object Marginal(string variableName)
		{
			if (variableName=="vdouble0") {
				return this.Vdouble0Marginal();
			}
			if (variableName=="vbool__0") {
				return this.Vbool__0Marginal();
			}
			throw new ArgumentException("This class was not built to infer "+variableName);
		}

		/// <summary>Get the marginal distribution (computed up to this point) of a variable, converted to type T</summary>
		/// <typeparam name="T">The distribution type.</typeparam>
		/// <param name="variableName">Name of the variable in the generated code</param>
		/// <returns>The marginal distribution computed up to this point</returns>
		/// <remarks>Execute, Update, or Reset must be called first to set the value of the marginal.</remarks>
		public T Marginal<T>(string variableName)
		{
			return Distribution.ChangeType<T>(this.Marginal(variableName));
		}

		/// <summary>Get the query-specific marginal distribution of a variable.</summary>
		/// <param name="variableName">Name of the variable in the generated code</param>
		/// <param name="query">QueryType name. For example, GibbsSampling answers 'Marginal', 'Samples', and 'Conditionals' queries</param>
		/// <remarks>Execute, Update, or Reset must be called first to set the value of the marginal.</remarks>
		public object Marginal(string variableName, string query)
		{
			if (query=="Marginal") {
				return this.Marginal(variableName);
			}
			throw new ArgumentException(((("This class was not built to infer \'"+variableName)+"\' with query \'")+query)+"\'");
		}

		/// <summary>Get the query-specific marginal distribution of a variable, converted to type T</summary>
		/// <typeparam name="T">The distribution type.</typeparam>
		/// <param name="variableName">Name of the variable in the generated code</param>
		/// <param name="query">QueryType name. For example, GibbsSampling answers 'Marginal', 'Samples', and 'Conditionals' queries</param>
		/// <remarks>Execute, Update, or Reset must be called first to set the value of the marginal.</remarks>
		public T Marginal<T>(string variableName, string query)
		{
			return Distribution.ChangeType<T>(this.Marginal(variableName, query));
		}

		private void OnProgressChanged(ProgressChangedEventArgs e)
		{
			// Make a temporary copy of the event to avoid a race condition
			// if the last subscriber unsubscribes immediately after the null check and before the event is raised.
			EventHandler<ProgressChangedEventArgs> handler = this.ProgressChanged;
			if (handler!=null) {
				handler(this, e);
			}
		}

		/// <summary>Reset all messages to their initial values.  Sets NumberOfIterationsDone to 0.</summary>
		public void Reset()
		{
			this.Execute(0);
		}

		/// <summary>Set the observed value of the specified variable.</summary>
		/// <param name="variableName">Variable name</param>
		/// <param name="value">Observed value</param>
		public void SetObservedValue(string variableName, object value)
		{
			if (variableName=="vbool__0") {
				this.vbool__0 = (bool[])value;
				return ;
			}
			throw new ArgumentException("Not an observed variable name: "+variableName);
		}

		/// <summary>Update all marginals, by iterating message-passing an additional number of times</summary>
		/// <param name="additionalIterations">The number of iterations that should be executed, starting from the current message state.  Messages are not reset, even if observed values have changed.</param>
		public void Update(int additionalIterations)
		{
			this.Execute(checked(this.numberOfIterationsDone+additionalIterations), false);
		}

		/// <summary>
		/// Returns the marginal distribution for 'vbool__0' given by the current state of the
		/// message passing algorithm.
		/// </summary>
		/// <returns>The marginal distribution</returns>
		public DistributionStructArray<Bernoulli,bool> Vbool__0Marginal()
		{
			return this.vbool__0_marginal_F;
		}

		/// <summary>
		/// Returns the marginal distribution for 'vdouble0' given by the current state of the
		/// message passing algorithm.
		/// </summary>
		/// <returns>The marginal distribution</returns>
		public Beta Vdouble0Marginal()
		{
			return this.vdouble0_marginal_F;
		}

		#endregion

		#region Events
		/// <summary>Event that is fired when the progress of inference changes, typically at the end of one iteration of the inference algorithm.</summary>
		public event EventHandler<ProgressChangedEventArgs> ProgressChanged;
		#endregion

	}

}
