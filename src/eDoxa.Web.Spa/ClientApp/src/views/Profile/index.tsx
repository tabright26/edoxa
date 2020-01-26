import React, { Suspense, FunctionComponent } from "react";
import { Route, Switch, Redirect } from "react-router-dom";
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
  getUserProfileOverviewPath,
  getUserProfileTransactionHistoryPath,
  getUserProfilePaymentMethodsPath,
  getUserProfileGamesPath,
  getUserProfileSecurityPath,
  getUserProfileDetailsPath,
  getError404Path,
  getUserProfilePromotionalCodePath,
  getUserProfileChallengeHistoryPath
} from "utils/coreui/constants";

const ProfileOverview = React.lazy(() => import("./Overview"));
const ProfileDetails = React.lazy(() => import("./Details"));
const ProfileSecurity = React.lazy(() => import("./Security"));
const ProfileConnections = React.lazy(() => import("./Games"));
const ProfilePromotionalCode = React.lazy(() => import("./PromotionalCode"));
const ProfilePaymentMethods = React.lazy(() => import("./PaymentMethods"));
const ProfileTransactionHistory = React.lazy(() =>
  import("./TransactionHistory")
);
const ProfileChallengeHistory = React.lazy(() => import("./ChallengeHistory"));

const Profile: FunctionComponent = () => (
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
          <strong>Arena</strong>
        </CardHeader>
        <ListGroup flush>
          <LinkContainer to={getUserProfileChallengeHistoryPath()}>
            <ListGroupItem>Challenge History</ListGroupItem>
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
          <LinkContainer to={getUserProfilePromotionalCodePath()}>
            <ListGroupItem>Promotional Code</ListGroupItem>
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
            component={ProfileOverview}
          />
          <Route<RouteProps>
            path={getUserProfileDetailsPath()}
            exact
            name="Profile Details"
            component={ProfileDetails}
          />
          <Route<RouteProps>
            path={getUserProfileSecurityPath()}
            exact
            name="Security"
            component={ProfileSecurity}
          />
          <Route<RouteProps>
            path={getUserProfileGamesPath()}
            exact
            name="Connections"
            component={ProfileConnections}
          />
          <Route<RouteProps>
            path={getUserProfilePaymentMethodsPath()}
            exact
            name="Payment Methods"
            component={ProfilePaymentMethods}
          />
          <Route<RouteProps>
            path={getUserProfileTransactionHistoryPath()}
            exact
            name="Transaction History"
            component={ProfileTransactionHistory}
          />
          <Route<RouteProps>
            path={getUserProfilePromotionalCodePath()}
            exact
            name="Promotional Code"
            component={ProfilePromotionalCode}
          />
          <Route<RouteProps>
            path={getUserProfileChallengeHistoryPath()}
            exact
            name="Challenge History"
            component={ProfileChallengeHistory}
          />
          <Redirect to={getError404Path()} />
        </Switch>
      </Suspense>
    </Col>
  </Row>
);

export default Profile;
