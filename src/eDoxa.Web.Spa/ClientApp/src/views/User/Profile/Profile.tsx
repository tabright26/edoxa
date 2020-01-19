import React, { Suspense, FunctionComponent } from "react";
import { Route, Switch, Redirect, RouteComponentProps } from "react-router-dom";
import {
  Card,
  CardHeader,
  Col,
  Row,
  ListGroup,
  ListGroupItem
} from "reactstrap";
import { RouteProps } from "utils/router/types";
import { LinkContainer } from "react-router-bootstrap";
import { Loading } from "components/Shared/Loading";
import {
  getUserProfilePath,
  getUserProfileOverviewPath,
  getUserProfileTransactionHistoryPath,
  getUserProfilePaymentMethodsPath,
  getUserProfileGamesPath,
  getUserProfileSecurityPath,
  getUserProfileDetailsPath,
  getError404Path
} from "utils/coreui/constants";

const ProfileOverview: FunctionComponent<RouteComponentProps> = React.lazy(() =>
  import("./Overview/Overview")
);
const ProfileDetails: FunctionComponent<RouteComponentProps> = React.lazy(() =>
  import("./Details/Details")
);
const ProfileSecurity: FunctionComponent<RouteComponentProps> = React.lazy(() =>
  import("./Security/Security")
);
const ProfileConnections: FunctionComponent<RouteComponentProps> = React.lazy(
  () => import("./Games/Games")
);
const ProfilePaymentMethods: FunctionComponent<RouteComponentProps> = React.lazy(
  () => import("./PaymentMethods")
);
const ProfileTransactionHistory: FunctionComponent<RouteComponentProps> = React.lazy(
  () => import("./TransactionHistory")
);

const Profile: FunctionComponent<RouteComponentProps> = ({ match }) => (
  <Row>
    <Col xs="12" sm="12" md="4" lg="3" xl="2">
      <Card className="mt-4">
        <CardHeader>
          <strong>Profile</strong>
        </CardHeader>
        <ListGroup flush>
          <LinkContainer to={getUserProfileOverviewPath()}>
            <ListGroupItem>Overview</ListGroupItem>
          </LinkContainer>
          <LinkContainer to={getUserProfileDetailsPath()}>
            <ListGroupItem>Details</ListGroupItem>
          </LinkContainer>
          <LinkContainer to={getUserProfileSecurityPath()}>
            <ListGroupItem>Security</ListGroupItem>
          </LinkContainer>
        </ListGroup>
      </Card>
      <Card>
        <CardHeader>
          <strong>Cashier</strong>
        </CardHeader>
        <ListGroup flush>
          <LinkContainer to={getUserProfilePaymentMethodsPath()}>
            <ListGroupItem>Payment Methods</ListGroupItem>
          </LinkContainer>
          <LinkContainer to={getUserProfileTransactionHistoryPath()}>
            <ListGroupItem>Transaction History</ListGroupItem>
          </LinkContainer>
        </ListGroup>
      </Card>
      <Card>
        <CardHeader>
          <strong>Connections</strong>
        </CardHeader>
        <ListGroup flush>
          <LinkContainer to={getUserProfileGamesPath()}>
            <ListGroupItem>Games</ListGroupItem>
          </LinkContainer>
        </ListGroup>
      </Card>
    </Col>
    <Col xs="12" sm="12" md="8" lg="7" xl="6">
      <Suspense fallback={<Loading />}>
        <Switch>
          <Route<RouteProps>
            path={getUserProfileOverviewPath()}
            exact
            name="Profile Overview"
            render={props => <ProfileOverview {...props} />}
          />
          <Route<RouteProps>
            path={getUserProfileDetailsPath()}
            exact
            name="Profile Details"
            render={props => <ProfileDetails {...props} />}
          />
          <Route<RouteProps>
            path={getUserProfileSecurityPath()}
            exact
            name="Security"
            render={props => <ProfileSecurity {...props} />}
          />
          <Route<RouteProps>
            path={getUserProfileGamesPath()}
            exact
            name="Connections"
            render={props => <ProfileConnections {...props} />}
          />
          <Route<RouteProps>
            path={getUserProfilePaymentMethodsPath()}
            exact
            name="Payment Methods"
            render={props => <ProfilePaymentMethods {...props} />}
          />
          <Route<RouteProps>
            path={getUserProfileTransactionHistoryPath()}
            exact
            name="Transaction History"
            render={props => <ProfileTransactionHistory {...props} />}
          />
          <Redirect
            from={getUserProfilePath()}
            to={getUserProfileOverviewPath()}
          />
          <Redirect to={getError404Path()} />
        </Switch>
      </Suspense>
    </Col>
  </Row>
);

export default Profile;
