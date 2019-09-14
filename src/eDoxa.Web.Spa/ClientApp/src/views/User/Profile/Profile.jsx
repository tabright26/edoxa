import React, { Suspense } from "react";
import { Route, Switch, Redirect } from "react-router-dom";
import { Col, Row, ListGroup, ListGroupItem } from "reactstrap";
import { LinkContainer } from "react-router-bootstrap";
import Loading from "../../../components/Shared/Loading";

const ProfileOverview = React.lazy(() => import("./Overview/Overview"));
const ProfileDetails = React.lazy(() => import("./Details/Details"));
const ProfileSecurity = React.lazy(() => import("./Security/Security"));
const ProfileConfidentiality = React.lazy(() => import("./Confidentiality/Confidentiality"));
const ProfileConnections = React.lazy(() => import("./Connections/Connections"));
const ProfileCashier = React.lazy(() => import("./Cashier/"));
const ProfileTransactionHistory = React.lazy(() => import("./TransactionHistory/TransactionHistory"));

const Profile = ({ match }) => (
  <Row>
    <Col xs="12" sm="12" md="4" lg="3" xl="2">
      <ListGroup className="my-4">
        <LinkContainer to={`${match.url}/overview`}>
          <ListGroupItem>Profile Overview</ListGroupItem>
        </LinkContainer>
        <LinkContainer to={`${match.url}/details`}>
          <ListGroupItem>Profile Details</ListGroupItem>
        </LinkContainer>
        <LinkContainer to={`${match.url}/security`}>
          <ListGroupItem>Security</ListGroupItem>
        </LinkContainer>
        <LinkContainer to={`${match.url}/confidentiality`}>
          <ListGroupItem>Confidentiality</ListGroupItem>
        </LinkContainer>
        <LinkContainer to={`${match.url}/connections`}>
          <ListGroupItem>Connections</ListGroupItem>
        </LinkContainer>
        <LinkContainer to={`${match.url}/payment-methods`}>
          <ListGroupItem>Payment Methods</ListGroupItem>
        </LinkContainer>
        <LinkContainer to={`${match.url}/transaction-history`}>
          <ListGroupItem>Transaction History</ListGroupItem>
        </LinkContainer>
      </ListGroup>
    </Col>
    <Col xs="12" sm="12" md="8" lg="7" xl="6">
      <Suspense fallback={<Loading.Default />}>
        <Switch>
          <Route path={`${match.url}/overview`} exact name="Profile Overview" render={props => <ProfileOverview {...props} />} />
          <Route path={`${match.url}/details`} exact name="Profile Details" render={props => <ProfileDetails {...props} />} />
          <Route path={`${match.url}/security`} exact name="Security" render={props => <ProfileSecurity {...props} />} />
          <Route path={`${match.url}/confidentiality`} exact name="Confidentiality" render={props => <ProfileConfidentiality {...props} />} />
          <Route path={`${match.url}/connections`} exact name="Connections" render={props => <ProfileConnections {...props} />} />
          <Route path={`${match.url}/payment-methods`} exact name="Payment Methods" render={props => <ProfileCashier {...props} />} />
          <Route path={`${match.url}/transaction-history`} exact name="Transaction History" render={props => <ProfileTransactionHistory {...props} />} />
          <Redirect from={`${match.url}`} to={`${match.url}/overview`} />
        </Switch>
      </Suspense>
    </Col>
  </Row>
);

export default Profile;
