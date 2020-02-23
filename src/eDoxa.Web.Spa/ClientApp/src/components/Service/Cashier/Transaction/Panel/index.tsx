import React, { useState, useEffect, FunctionComponent } from "react";
import { Card } from "reactstrap";
import List from "components/Service/Cashier/Transaction/List";
import { Paginate } from "components/Shared/Paginate";
import { compose } from "recompose";
import { UserTransactionState } from "store/root/user/transactionHistory/types";
import {
  CurrencyType,
  TransactionType,
  TransactionStatus,
  Transaction
} from "types/cashier";
import { Loading } from "components/Shared/Loading";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";

const pageSize = 10;

interface StateProps {
  transactionHistory: UserTransactionState;
}

interface OwnProps {
  currency?: CurrencyType | null;
  type?: TransactionType | null;
  status?: TransactionStatus | null;
}

type InnerProps = StateProps;

type OutterProps = OwnProps;

type Props = InnerProps & OutterProps;

const Panel: FunctionComponent<Props> = ({
  transactionHistory: { data, loading }
}) => {
  const [transactions, setTransactions] = useState<Transaction[]>([]);
  useEffect(() => {
    setTransactions(data.slice(0, pageSize));
  }, [data]);
  return (
    <>
      <Card className="card-accent-primary mb-3">
        {loading ? <Loading /> : <List transactions={transactions} />}
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

const enhance = compose<InnerProps, OutterProps>(connect(mapStateToProps));

export default enhance(Panel);
