import React, { useState, useEffect, FunctionComponent } from "react";
import { Card } from "reactstrap";
import TransactionList from "components/User/Transaction/List";
import Paginate from "components/Shared/Paginate";
import { compose } from "recompose";
import { UserTransactionState } from "store/root/user/transactionHistory/types";
import { Currency, TransactionType, TransactionStatus } from "types";
import Loading from "components/Shared/Loading";
import { connect, MapStateToProps, MapDispatchToProps } from "react-redux";
import { RootState } from "store/types";
import { loadUserTransactionHistory } from "store/actions/cashier";

const pageSize = 10;

interface StateProps {
  transactionHistory: UserTransactionState;
}

interface DispatchProps {
  loadUserTransactionHistory: () => void;
}

interface OwnProps {
  currency?: Currency | null;
  type?: TransactionType | null;
  status?: TransactionStatus | null;
}

type InnerProps = StateProps & DispatchProps;

type OutterProps = OwnProps;

type Props = InnerProps & OutterProps;

const FilteredTransactions: FunctionComponent<Props> = ({
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
  StateProps,
  OwnProps,
  RootState
> = state => {
  return {
    transactionHistory: state.root.user.transactionHistory
  };
};

const mapDispatchToProps: MapDispatchToProps<DispatchProps, OwnProps> = (
  dispatch: any,
  ownProps
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

const enhance = compose<InnerProps, OutterProps>(
  connect(mapStateToProps, mapDispatchToProps)
);

export default enhance(FilteredTransactions);
