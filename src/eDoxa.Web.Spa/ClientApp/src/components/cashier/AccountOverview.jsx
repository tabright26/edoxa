import React, { Component } from 'react';
import { connect } from 'react-redux';
import {
  Container,
  Row,
  Col,
  Card,
  Table,
  Alert,
  Button
} from 'react-bootstrap';

import Moment from 'react-moment';

import CurrencyFormat from '../Shared/Formaters/CurrencyFormat';

import {
  loadUserAccountTransactions,
  loadUserStripeCards,
  loadUserStripeBankAccounts
} from '../../store/actions/userAccountActions';

import UserAccountBalanceMoneyIndex from '../User/Account/Balance/Money/Index';
import UserAccountBalanceTokenIndex from '../User/Account/Balance/Token/Index';

class CashierOverview extends Component {
  componentDidMount() {
    this.props.actions.loadUserAccountTransactions();
    this.props.actions.loadUserStripeCards();
    this.props.actions.loadUserStripeBankAccounts();
  }

  render() {
    const { cards, transactions, hasBankAccount } = this.props;
    return (
      <Container>
        <Row>
          <Col xs="12">
            <h1 className="text-light mt-5">Account Overview</h1>
            <hr className="border" />
          </Col>
          <Col>
            <Alert variant="primary" className="mb-0">
              <Alert.Heading>Your account is not verified!</Alert.Heading>
              <div className="d-flex">
                <p className="mb-0">
                  Your account is currently not verified. This may prevent you
                  from accessing certain features of this website...
                </p>
                <Button
                  variant="primary"
                  size="sm"
                  className="mt-auto ml-auto text-white"
                >
                  Verify
                </Button>
              </div>
            </Alert>
          </Col>
        </Row>
        <Row>
          <Col xs="6">
            <Card bg="dark" className="text-light mt-3">
              <Card.Header as="h3">Money</Card.Header>
              <Card.Body>
                <UserAccountBalanceMoneyIndex />
              </Card.Body>
            </Card>
          </Col>
          <Col xs="6">
            <Card bg="dark" className="text-light mt-3">
              <Card.Header as="h3">Tokens</Card.Header>
              <Card.Body>
                <UserAccountBalanceTokenIndex />
              </Card.Body>
            </Card>
          </Col>
        </Row>
        <Row>
          <Col xs="12">
            <h3 className="text-light mt-4">Transactions</h3>
            <hr className="border" />
          </Col>
          <Col>
            <Card bg="dark">
              <Table
                variant="dark"
                borderless
                hover
                responsive
                striped
                className="mb-0"
              >
                <thead>
                  <tr>
                    <th>Id</th>
                    <th>Date</th>
                    <th>Amount</th>
                    <th>Description</th>
                    <th>Type</th>
                    <th>Status</th>
                  </tr>
                </thead>
                <tbody>
                  {transactions
                    ? transactions
                        .sort((left, right) =>
                          left.timestamp < right.timestamp ? -1 : 1
                        )
                        .map((transaction, index) => (
                          <tr key={index}>
                            <td>{transaction.id}</td>
                            <td>
                              <Moment unix format="ll">
                                {transaction.timestamp}
                              </Moment>
                            </td>
                            <td>
                              <CurrencyFormat
                                currency={transaction.currency}
                                amount={transaction.amount}
                              />
                            </td>
                            <td>{transaction.description}</td>
                            <td>{transaction.type}</td>
                            <td>{transaction.status}</td>
                          </tr>
                        ))
                    : []}
                </tbody>
              </Table>
            </Card>
          </Col>
        </Row>
        <Row>
          <Col xs="12">
            <h3 className="text-light mt-4">Payment Methods</h3>
            <hr className="border" />
          </Col>
          <Col>
            <Card bg="dark">
              <Table
                variant="dark"
                responsive
                borderless
                striped
                hover
                className="mb-0"
              >
                <thead>
                  <tr>
                    <th>Brand</th>
                    <th>Last4</th>
                    <th>Expiration</th>
                  </tr>
                </thead>
                <tbody>
                  {cards.map((card, index) => (
                    <tr key={index}>
                      <td>{card.brand}</td>
                      <td>**** {card.last4}</td>
                      <td>
                        {card.exp_month}/{card.exp_year}
                      </td>
                    </tr>
                  ))}
                </tbody>
              </Table>
            </Card>
          </Col>
        </Row>
        <Row>
          <Col>
            {hasBankAccount ? (
              <Alert variant="success" className="mt-4 mb-0">
                <Alert.Heading>Bank Account</Alert.Heading>
                <div className="d-flex">
                  <p className="mb-0">
                    To activate cash withdrawal, you must add your bank account
                    information. The funds will then be transferred directly to
                    your account from eDoxa.
                  </p>
                  <Button
                    variant="danger"
                    size="sm"
                    className="my-auto mt-auto"
                  >
                    Remove
                  </Button>
                </div>
              </Alert>
            ) : (
              <Alert variant="info" className="mt-3 mb-0">
                <Alert.Heading>Bank Account</Alert.Heading>
                <div className="d-flex">
                  <p className="mb-0">
                    To activate cash withdrawal, you must add your bank account
                    information. The funds will then be transferred directly to
                    your account from eDoxa.
                  </p>
                  <Button variant="info" size="sm" className="mt-auto ml-auto">
                    Link
                  </Button>
                </div>
              </Alert>
            )}
          </Col>
        </Row>
      </Container>
    );
  }
}

const mapStateToProps = state => {
  return {
    transactions: state.cashier.transactions,
    cards: state.cashier.cards,
    account: state.cashier.account
  };
};

const mapDispatchToProps = dispatch => {
  return {
    actions: {
      loadUserAccountTransactions: () =>
        dispatch(loadUserAccountTransactions()),
      loadUserStripeCards: () => dispatch(loadUserStripeCards()),
      loadUserStripeBankAccounts: () => dispatch(loadUserStripeBankAccounts())
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(CashierOverview);
