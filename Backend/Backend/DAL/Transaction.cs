using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DAL
{
    public class Transaction : IDisposable
    {
        private bool disposed;
        private bool isCommited;

        private TransactionScope scope;

        public Transaction()
        {
            this.Start(IsolationLevel.Serializable);
        }

        public Transaction(IsolationLevel isolationLevel)
        {
            this.Start(isolationLevel);
        }

        private void Start(IsolationLevel isolationLevel)
        {
            var transactionOptions = new TransactionOptions();
            transactionOptions.IsolationLevel = isolationLevel;

            if (this.disposed) throw new ObjectDisposedException(this.ToString());
            this.scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions);
        }

        private void Commit()
        {
            if (this.disposed) throw new ObjectDisposedException(this.ToString());
            if (this.scope == null) throw new InvalidOperationException(this.ToString());
            this.isCommited = true;
            this.scope.Complete();
        }

        public void Dispose()
        {
            if (this.scope != null)
            {
                if (!this.isCommited) this.Commit();

                this.scope.Dispose();
                this.scope = null;
            }
            this.disposed = true;
        }
    }
}
