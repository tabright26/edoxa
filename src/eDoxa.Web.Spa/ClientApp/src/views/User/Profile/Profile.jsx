import React, { Suspense } from "react";
import { Route, Switch, Redirect } from "react-router-dom";
import { Card, CardHeader, Col, Row, ListGroup, ListGroupItem } from "reactstrap";
import { LinkContainer } from "react-router-bootstrap";
import Loading from "components/Shared/Override/Loading";

const ProfileOverview = React.lazy(() => import("./Overview/Overview"));
const ProfileDetails = React.lazy(() => import("./Details/Details"));
const ProfileSecurity = React.lazy(() => import("./Security/Security"));
const ProfileConnections = React.lazy(() => import("./Connections/Connections"));
const ProfilePaymentMethods = React.lazy(() => import("./Cashier/PaymentMethods"));
const ProfileTransactionHistory = React.lazy(() => import("./Cashier/TransactionHistory"));

const Profile = ({ match }) => (
  <Row>
    <Col xs="12" sm="12" md="4" lg="3" xl="2">
      <Card>
        <ListGroup flush>
          <LinkContainer to={`${match.url}/overview`}>
            <ListGroupItem>Overview</ListGroupItem>
          </LinkContainer>
        </ListGroup>
      </Card>
      <Card>
        <CardHeader>
          <strong>Profile</strong>
        </CardHeader>
        <ListGroup flush>
          <LinkContainer to={`${match.url}/details`}>
            <ListGroupItem>Details</ListGroupItem>
          </LinkContainer>
          <LinkContainer to={`${match.url}/security`}>
            <ListGroupItem>Security</ListGroupItem>
          </LinkContainer>
        </ListGroup>
      </Card>
      <Card>
        <CardHeader>
          <strong>Cashier</strong>
        </CardHeader>
        <ListGroup flush>
          <LinkContainer to={`${match.url}/cashier/payment-methods`}>
            <ListGroupItem>Payment Methods</ListGroupItem>
          </LinkContainer>
          <LinkContainer to={`${match.url}/cashier/transaction-history`}>
            <ListGroupItem>Transaction History</ListGroupItem>
          </LinkContainer>
        </ListGroup>
      </Card>
      <Card>
        <CardHeader>
          <strong>Connections</strong>
        </CardHeader>
        <ListGroup flush>
          <LinkContainer to={`${match.url}/connections/games`}>
            <ListGroupItem>Games</ListGroupItem>
          </LinkContainer>
        </ListGroup>
      </Card>
    </Col>
    <Col xs="12" sm="12" md="8" lg="7" xl="6">
      <Suspense fallback={<Loading />}>
        <Switch>
          <Route path={`${match.url}/overview`} exact name="Profile Overview" render={props => <ProfileOverview {...props} />} />
          <Route path={`${match.url}/details`} exact name="Profile Details" render={props => <ProfileDetails {...props} />} />
          <Route path={`${match.url}/security`} exact name="Security" render={props => <ProfileSecurity {...props} />} />
          <Route path={`${match.url}/connections/games`} exact name="Connections" render={props => <ProfileConnections {...props} />} />
          <Route path={`${match.url}/cashier/payment-methods`} exact name="Payment Methods" render={props => <ProfilePaymentMethods {...props} />} />
          <Route path={`${match.url}/cashier/transaction-history`} exact name="Transaction History" render={props => <ProfileTransactionHistory {...props} />} />
          <Redirect from={`${match.url}`} to={`${match.url}/overview`} />
        </Switch>
      </Suspense>
    </Col>
  </Row>
);

export default Profile;
