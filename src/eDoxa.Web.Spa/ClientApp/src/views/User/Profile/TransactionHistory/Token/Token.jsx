import React, { useState, useEffect } from "react";
import { Card, CardHeader } from "reactstrap";
import { connectUserAccountTransactions } from "store/user/account/transactions/container";
import TokenIcon from "icons/Token";
import TransactionList from "components/User/Account/Transaction/List";
import Paginate from "components/Shared/Paginate";

// TODO: Paging logic must be done server side.
const TokenTransactionHistory = ({ actions, transactions }) => {
  const pageSize = 4;
  const [data, setDate] = useState([]);
  useEffect(() => {
    setDate(transactions.slice(0, pageSize));
  }, [transactions]);
  return (
    <>
      <Card className="card-accent-primary card-table-row-5 mt-4 mb-2">
        <CardHeader>
          <TokenIcon className="text-primary" /> <strong>TOKEN TRANSACTIONS</strong>
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

export default connectUserAccountTransactions("token")(TokenTransactionHistory);
