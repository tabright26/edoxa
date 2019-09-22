import React, { useState, useEffect } from "react";
import { Card, CardHeader } from "reactstrap";
import connectUserAccountTransactions from "containers/connectUserAccountTransactions";
import MoneyIcon from "icons/Money";
import TransactionList from "components/User/Account/Transaction/List";
import Paginate from "components/Shared/Paginate";

// TODO: Paging logic must be done server side.
const MoneyTransactionHistory = ({ actions, transactions }) => {
  const pageSize = 5;
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

export default connectUserAccountTransactions("money")(MoneyTransactionHistory);
