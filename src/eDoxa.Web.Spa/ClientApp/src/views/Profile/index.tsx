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
  getProfileOverviewPath,
  getProfileTransactionHistoryPath,
  getProfilePaymentMethodsPath,
  getProfileGamesPath,
  getProfileSecurityPath,
  getProfileDetailsPath,
  getError404Path,
  getProfilePromotionalCodePath,
  getProfileChallengeHistoryPath
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
          <LinkContainer to={getProfileOverviewPath()}>
            <ListGroupItem>Overview</ListGroupItem>
          </LinkContainer>
          <LinkContainer to={getProfileDetailsPath()}>
            <ListGroupItem>Details</ListGroupItem>
          </LinkContainer>
          <LinkContainer to={getProfileSecurityPath()}>
            <ListGroupItem>Security</ListGroupItem>
          </LinkContainer>
        </ListGroup>
      </Card>
      <Card>
        <CardHeader>
          <strong>Arena</strong>
        </CardHeader>
        <ListGroup flush>
          <LinkContainer to={getProfileChallengeHistoryPath()}>
            <ListGroupItem>Challenge History</ListGroupItem>
          </LinkContainer>
        </ListGroup>
      </Card>
      <Card>
        <CardHeader>
          <strong>Cashier</strong>
        </CardHeader>
        <ListGroup flush>
          <LinkContainer to={getProfilePaymentMethodsPath()}>
            <ListGroupItem>Payment Methods</ListGroupItem>
          </LinkContainer>
          <LinkContainer to={getProfileTransactionHistoryPath()}>
            <ListGroupItem>Transaction History</ListGroupItem>
          </LinkContainer>
          <LinkContainer to={getProfilePromotionalCodePath()}>
            <ListGroupItem>Promotional Code</ListGroupItem>
          </LinkContainer>
        </ListGroup>
      </Card>
      <Card>
        <CardHeader>
          <strong>Connections</strong>
        </CardHeader>
        <ListGroup flush>
          <LinkContainer to={getProfileGamesPath()}>
            <ListGroupItem>Games</ListGroupItem>
          </LinkContainer>
        </ListGroup>
      </Card>
    </Col>
    <Col xs="12" sm="12" md="8" lg="7" xl="6">
      <Suspense fallback={<Loading />}>
        <Switch>
          <Route<RouteProps>
            path={getProfileOverviewPath()}
            exact
            name="Profile Overview"
            component={ProfileOverview}
          />
          <Route<RouteProps>
            path={getProfileDetailsPath()}
            exact
            name="Profile Details"
            component={ProfileDetails}
          />
          <Route<RouteProps>
            path={getProfileSecurityPath()}
            exact
            name="Security"
            component={ProfileSecurity}
          />
          <Route<RouteProps>
            path={getProfileGamesPath()}
            exact
            name="Connections"
            component={ProfileConnections}
          />
          <Route<RouteProps>
            path={getProfilePaymentMethodsPath()}
            exact
            name="Payment Methods"
            component={ProfilePaymentMethods}
          />
          <Route<RouteProps>
            path={getProfileTransactionHistoryPath()}
            exact
            name="Transaction History"
            component={ProfileTransactionHistory}
          />
          <Route<RouteProps>
            path={getProfilePromotionalCodePath()}
            exact
            name="Promotional Code"
            component={ProfilePromotionalCode}
          />
          <Route<RouteProps>
            path={getProfileChallengeHistoryPath()}
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
