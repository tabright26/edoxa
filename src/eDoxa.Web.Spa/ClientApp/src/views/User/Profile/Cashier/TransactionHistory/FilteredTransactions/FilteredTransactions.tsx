import React, { useState, useEffect, FunctionComponent } from "react";
import { Card, CardHeader } from "reactstrap";
import { withUserAccountTransactions } from "store/root/user/account/transactions/container";
import TransactionList from "components/User/Account/Transaction/List";
import Paginate from "components/Shared/Paginate";
import { compose } from "recompose";
import { UserAccountTransactionsState } from "store/root/user/account/transactions/types";
import { Currency, TransactionType, TransactionStatus } from "types";
import Loading from "components/Shared/Loading";

const pageSize = 10;

interface FilteredTransactionsInnerProps {
  transactions: UserAccountTransactionsState;
}

interface FilteredTransactionsOutterProps {
  currency?: Currency | null;
  type?: TransactionType | null;
  status?: TransactionStatus | null;
}

type FilteredTransactionsProps = FilteredTransactionsInnerProps & FilteredTransactionsOutterProps;

const FilteredTransactions: FunctionComponent<FilteredTransactionsProps> = ({ transactions: { data, error, loading } }) => {
  const [transactions, setTransactions] = useState([]);
  useEffect(() => {
    setTransactions(data.slice(0, pageSize));
  }, [data]);
  return (
    <>
      <Card className="card-accent-primary mt-4 mb-3">
        <CardHeader>Filtering fields</CardHeader>
        {loading ? <Loading /> : <TransactionList transactions={transactions} />}
      </Card>
      <Paginate
        pageSize={pageSize}
        totalItems={data.length}
        onPageChange={(currentPage, pageSize) => {
          const start = currentPage * pageSize;
          const end = start + pageSize;
          setTransactions(data.slice(start, end));
        }}
      />
    </>
  );
};

const enhance = compose<FilteredTransactionsInnerProps, FilteredTransactionsOutterProps>(withUserAccountTransactions);

export default enhance(FilteredTransactions);
