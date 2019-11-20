import "./App.scss";
import React, { Suspense, FunctionComponent } from "react";
import { Route, Switch, Redirect, RouteComponentProps } from "react-router-dom";
import { ConnectedRouter as Router } from "connected-react-router";
import { history } from "utils/router/config";
import UserAccountModal from "modals/User/Account";
import MatchScoreModal from "modals/Arena/Challenge/Participant/Match/Score";
import Loading from "components/Shared/Loading";
import { RouteProps } from "utils/router/types";
import StripePaymentMethodModal from "modals/Payment/Stripe/PaymentMethod";
import { Elements } from "react-stripe-elements";
import { ApplicationPaths } from "utils/oidc/ApiAuthorizationConstants";

const ApiAuthorizationRoutes: FunctionComponent<RouteComponentProps> = React.lazy(
  () => import("utils/oidc/ApiAuthorizationRoutes")
);
const ErrorPage401: FunctionComponent<RouteComponentProps> = React.lazy(() =>
  import("components/Shared/ErrorPage/401")
);
const ErrorPage403: FunctionComponent<RouteComponentProps> = React.lazy(() =>
  import("components/Shared/ErrorPage/403")
);
const ErrorPage404: FunctionComponent<RouteComponentProps> = React.lazy(() =>
  import("components/Shared/ErrorPage/404")
);
const ErrorPage500: FunctionComponent<RouteComponentProps> = React.lazy(() =>
  import("components/Shared/ErrorPage/500")
);
const EmailConfirm: FunctionComponent<RouteComponentProps> = React.lazy(() =>
  import("views/User/Email/Comfirm")
);
const PasswordForgot: FunctionComponent<RouteComponentProps> = React.lazy(() =>
  import("views/User/Password/Forgot")
);
const PasswordReset: FunctionComponent<RouteComponentProps> = React.lazy(() =>
  import("views/User/Password/Reset")
);

const DefaultLayout: FunctionComponent<any> = React.lazy(() =>
  import("components/App/Layout/Default")
);
const NoneLayout: FunctionComponent<any> = React.lazy(() =>
  import("components/App/Layout/None")
);

const App = () => (
  <>
    <Elements>
      <StripePaymentMethodModal.Create />
    </Elements>
    <StripePaymentMethodModal.Update />
    <StripePaymentMethodModal.Delete />
    <UserAccountModal.Deposit />
    <UserAccountModal.Withdrawal />
    <MatchScoreModal />
    <Router history={history}>
      <Suspense fallback={<Loading />}>
        <Switch>
          <Route<any>
            path={ApplicationPaths.ApiAuthorizationPrefix}
            component={props => <ApiAuthorizationRoutes {...props} />}
          />
          <Route<RouteProps>
            exact
            path="/errors/401"
            name="Error 401"
            render={props => <ErrorPage401 {...props} />}
          />
          <Route<RouteProps>
            exact
            path="/errors/403"
            name="Error 403"
            render={props => <ErrorPage403 {...props} />}
          />
          <Route<RouteProps>
            exact
            path="/errors/404"
            name="Error 404"
            render={props => <ErrorPage404 {...props} />}
          />
          <Route<RouteProps>
            exact
            path="/errors/500"
            name="Error 500"
            render={props => <ErrorPage500 {...props} />}
          />
          <Route<RouteProps>
            exact
            path="/email/confirm"
            name="Confirm Email"
            render={props => (
              <Suspense fallback={<Loading />}>
                <NoneLayout>
                  <EmailConfirm {...props} />
                </NoneLayout>
              </Suspense>
            )}
          />
          <Route<RouteProps>
            exact
            path="/password/forgot"
            name="Forgot Password"
            render={props => (
              <Suspense fallback={<Loading />}>
                <NoneLayout>
                  <PasswordForgot {...props} />
                </NoneLayout>
              </Suspense>
            )}
          />
          <Route<RouteProps>
            exact
            path="/password/reset"
            name="Reset Password"
            render={props => (
              <Suspense fallback={<Loading />}>
                <NoneLayout>
                  <PasswordReset {...props} />
                </NoneLayout>
              </Suspense>
            )}
          />
          <Route<RouteProps>
            path="/"
            name="Home"
            render={props => <DefaultLayout {...props} />}
          />
          <Redirect to="/errors/404" />
        </Switch>
      </Suspense>
    </Router>
  </>
);

export default App;
