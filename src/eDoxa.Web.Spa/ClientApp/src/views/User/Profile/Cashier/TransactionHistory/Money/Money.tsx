import React, { useState, useEffect, FunctionComponent } from "react";
import { Card, CardHeader } from "reactstrap";
import { withUserAccountTransactions } from "store/root/user/account/transactions/container";
import MoneyIcon from "icons/Money";
import TransactionList from "components/User/Account/Transaction/List";
import Paginate from "components/Shared/Override/Paginate";
import { compose } from "recompose";

// TODO: Paging logic must be done server side.
const MoneyTransactionHistory: FunctionComponent<any> = ({ actions, transactions }) => {
  const pageSize = 4;
  const [data, setDate] = useState([]);
  useEffect(() => {
    setDate(transactions.slice(0, pageSize));
  }, [transactions]);
  return (
    <>
      <Card className="card-accent-primary card-table-row-5 mt-4 mb-2">
        <CardHeader>
          <MoneyIcon className="text-primary" /> <strong>MONEY TRANSACTIONS</strong>
        </CardHeader>
        <TransactionList actions={actions} transactions={data} />
      </Card>
      <Paginate
        pageSize={pageSize}
        totalItems={transactions.length}
        onPageChange={(currentPage, pageSize) => {
          const start = currentPage * pageSize;
          const end = start + pageSize;
          setDate(transactions.slice(start, end));
        }}
      />
    </>
  );
};

const enhance = compose<any, any>(withUserAccountTransactions("money"));

export default enhance(MoneyTransactionHistory);
