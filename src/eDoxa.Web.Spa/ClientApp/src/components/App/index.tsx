import "./index.scss";
import React, { Suspense, FunctionComponent } from "react";
import { Route, Switch, Redirect } from "react-router-dom";
import { ConnectedRouter as Router } from "connected-react-router";
import { history } from "utils/router/history";
import TransactionModal from "components/Service/Cashier/Transaction/Modal";
import ChallengeParticipantMatchModal from "components/Service/Challenge/Participant/Match/Modal";
import { Loading } from "components/Shared/Loading";
import { RouteProps } from "utils/router/types";
import StripePaymentMethodModal from "components/Service/Payment/Stripe/PaymentMethod/Modal";
import { ApplicationPaths } from "utils/oidc/ApiAuthorizationConstants";
import { initializeReactGA } from "utils/ga";
import {
  getError401Path,
  getError403Path,
  getError404Path,
  getError500Path,
  getHomePath,
  getEmailConfirmPath,
  getPasswordForgotPath,
  getPasswordResetPath,
  getAccountRegisterPath,
  getAccountLogoutPath,
  getAccountLoginPath,
  getDefaultPath,
  getWorkflowStepsPath
} from "utils/coreui/constants";
import { compose } from "recompose";
import { withWorkflow } from "utils/cookies/containers/withWorkflow";
import { withCashierStaticOptions } from "utils/options/containers/withCashierStaticOptions";
import { withGamesStaticOptions } from "utils/options/containers/withGamesStaticOptions";
import { withChallengesStaticOptions } from "utils/options/containers/withChallengesStaticOptions";
import { withIdentityStaticOptions } from "utils/options/containers/withIdentityStaticOptions";

initializeReactGA();

const ApiAuthorizationRoutes = React.lazy(() =>
  import("utils/oidc/ApiAuthorizationRoutes")
);
const Home = React.lazy(() => import("views/Home"));
const ErrorPage401 = React.lazy(() => import("views/Errors/401"));
const ErrorPage403 = React.lazy(() => import("views/Errors/403"));
const ErrorPage404 = React.lazy(() => import("views/Errors/404"));
const ErrorPage500 = React.lazy(() => import("views/Errors/500"));
const EmailConfirm = React.lazy(() => import("views/Account/Email/Comfirm"));
const PasswordForgot = React.lazy(() =>
  import("views/Account/Password/Forgot")
);
const PasswordReset = React.lazy(() => import("views/Account/Password/Reset"));
const DefaultLayout = React.lazy(() => import("components/App/Layout/Default"));
const NoneLayout = React.lazy(() => import("components/App/Layout/None"));
const Login = React.lazy(() => import("views/Account/Login"));
const Logout = React.lazy(() => import("views/Account/Logout"));
const Register = React.lazy(() => import("views/Account/Register"));
const Workflow = React.lazy(() => import("views/Workflow"));

const App: FunctionComponent = () => (
  <>
    <StripePaymentMethodModal.Create />
    <StripePaymentMethodModal.Update />
    <StripePaymentMethodModal.Delete />
    <TransactionModal.Deposit />
    <TransactionModal.Withdraw />
    <ChallengeParticipantMatchModal.Score />
    <Router history={history}>
      <Suspense fallback={<Loading />}>
        <Switch>
          <Route<any>
            path={ApplicationPaths.ApiAuthorizationPrefix}
            component={ApiAuthorizationRoutes}
          />
          <Route<RouteProps>
            exact
            path={getError401Path()}
            name="Error 401"
            render={() => <ErrorPage401 />}
          />
          <Route<RouteProps>
            exact
            path={getError403Path()}
            name="Error 403"
            render={() => <ErrorPage403 />}
          />
          <Route<RouteProps>
            exact
            path={getError404Path()}
            name="Error 404"
            render={() => <ErrorPage404 />}
          />
          <Route<RouteProps>
            exact
            path={getError500Path()}
            name="Error 500"
            render={() => <ErrorPage500 />}
          />
          <Route<RouteProps>
            exact
            path={getAccountLoginPath()}
            name="Login"
            render={() => <Login />}
          />
          <Route<RouteProps>
            exact
            path={getAccountLogoutPath()}
            name="Logout"
            render={() => <Logout />}
          />
          <Route<RouteProps>
            exact
            path={getAccountRegisterPath()}
            name="Register"
            render={() => <Register />}
          />
          <Route<RouteProps>
            exact
            path={getEmailConfirmPath()}
            name="Confirm Email"
            render={() => (
              <Suspense fallback={<Loading />}>
                <NoneLayout>
                  <EmailConfirm />
                </NoneLayout>
              </Suspense>
            )}
          />
          <Route<RouteProps>
            exact
            path={getPasswordForgotPath()}
            name="Forgot Password"
            render={() => (
              <Suspense fallback={<Loading />}>
                <NoneLayout>
                  <PasswordForgot />
                </NoneLayout>
              </Suspense>
            )}
          />
          <Route<RouteProps>
            exact
            path={getPasswordResetPath()}
            name="Reset Password"
            render={() => (
              <Suspense fallback={<Loading />}>
                <NoneLayout>
                  <PasswordReset />
                </NoneLayout>
              </Suspense>
            )}
          />
          <Route<RouteProps>
            exact
            path={getHomePath()}
            name="Home"
            component={Home}
          />
          <Route<RouteProps>
            path={getWorkflowStepsPath()}
            name="Workflow"
            render={() => <Workflow />}
          />
          <Route<RouteProps>
            path={getDefaultPath()}
            name="Default"
            render={() => <DefaultLayout />}
          />
          <Redirect to={getError404Path()} />
        </Switch>
      </Suspense>
    </Router>
  </>
);

const enhance = compose(
  withCashierStaticOptions,
  withGamesStaticOptions,
  withChallengesStaticOptions,
  withIdentityStaticOptions,
  withWorkflow
);

export default enhance(App);
