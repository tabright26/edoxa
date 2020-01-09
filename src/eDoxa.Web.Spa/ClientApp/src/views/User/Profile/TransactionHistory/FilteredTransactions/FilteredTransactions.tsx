import React, { useState, useEffect, FunctionComponent } from "react";
import { Card, CardHeader } from "reactstrap";
import TransactionList from "components/User/Transaction/List";
import Paginate from "components/Shared/Paginate";
import { compose } from "recompose";
import { UserTransactionState } from "store/root/user/transactionHistory/types";
import { Currency, TransactionType, TransactionStatus } from "types";
import Loading from "components/Shared/Loading";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { loadUserTransactionHistory } from "store/actions/cashier";

const pageSize = 10;

interface FilteredTransactionsInnerProps {
  transactionHistory: UserTransactionState;
  loadUserTransactionHistory: () => void;
}

interface FilteredTransactionsOutterProps {
  currency?: Currency | null;
  type?: TransactionType | null;
  status?: TransactionStatus | null;
}

type FilteredTransactionsProps = FilteredTransactionsInnerProps &
  FilteredTransactionsOutterProps;

interface UserAccountTransactionsStateProps {
  transactionHistory: UserTransactionState;
}

interface UserAccountTransactionsOwnProps {
  currency?: Currency | null;
  type?: TransactionType | null;
  status?: TransactionStatus | null;
}

const FilteredTransactions: FunctionComponent<FilteredTransactionsProps> = ({
  transactionHistory: { data, error, loading },
  loadUserTransactionHistory
}) => {
  useEffect((): void => {
    if (data.length === 0) {
      loadUserTransactionHistory();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  const [transactions, setTransactions] = useState([]);
  useEffect(() => {
    setTransactions(data.slice(0, pageSize));
  }, [data]);
  return (
    <>
      <Card className="card-accent-primary mt-4 mb-3">
        <CardHeader>Filtering fields</CardHeader>
        {loading ? (
          <Loading />
        ) : (
          <TransactionList transactions={transactions} />
        )}
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

const mapStateToProps: MapStateToProps<
  UserAccountTransactionsStateProps,
  UserAccountTransactionsOwnProps,
  RootState
> = state => {
  return {
    transactionHistory: state.root.user.transactionHistory
  };
};

const mapDispatchToProps = (
  dispatch: any,
  ownProps: UserAccountTransactionsOwnProps
) => {
  return {
    loadUserTransactionHistory: () =>
      dispatch(
        loadUserTransactionHistory(
          ownProps.currency,
          ownProps.type,
          ownProps.status
        )
      )
  };
};

const enhance = compose<
  FilteredTransactionsInnerProps,
  FilteredTransactionsOutterProps
>(connect(mapStateToProps, mapDispatchToProps));

export default enhance(FilteredTransactions);
