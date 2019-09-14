import React, { Fragment, Suspense } from "react";
import Loading from "../../../../components/Loading";
import { Container, Row, Col, Card, CardHeader, CardBody } from "reactstrap";
import Transaction from "../../../../components/User/Account/Transaction";
import withTransactionsHoc from "../../../../containers/withUserAccountTransactionHoc";

const AccountTransactions = ({ className, transactions, actions }) => (
  <Fragment>
    <h5 className="my-4">TRANSACTIONS HISTORY</h5>
    <Suspense fallback={<Loading.Default />}>
      <Container>
        <Row>
          <Col>
            <Card className={className}>
              <CardHeader>
                <strong>Deposits</strong>
              </CardHeader>
              <CardBody>
                {transactions.map((transaction, index) =>
                  transaction.type === "Deposit" ? <Transaction key={index} index={index + 1} actions={actions} transaction={transaction} length={transaction.length} /> : null
                )}
              </CardBody>
            </Card>
          </Col>
          <Col>
            <Card className={className}>
              <CardHeader>
                <strong>Withdrawals</strong>
              </CardHeader>
              <CardBody>
                {transactions.map((transaction, index) =>
                  transaction.type === "Withdrawal" ? <Transaction key={index} index={index + 1} actions={actions} transaction={transaction} length={transaction.length} /> : null
                )}
              </CardBody>
            </Card>
          </Col>
        </Row>
      </Container>
    </Suspense>
  </Fragment>
);

export default withTransactionsHoc(AccountTransactions);
