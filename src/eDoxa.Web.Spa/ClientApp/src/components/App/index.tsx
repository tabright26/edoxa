import "./index.scss";
import React, { Suspense, FunctionComponent } from "react";
import { Route, Switch, Redirect } from "react-router-dom";
import { ConnectedRouter as Router } from "connected-react-router";
import { history } from "utils/router/history";
import UserTransactionModal from "components/Transaction/Modal";
import ChallengeParticipantMatchModal from "components/Challenge/Participant/Match/Modal";
import { Loading } from "components/Shared/Loading";
import { RouteProps } from "utils/router/types";
import StripePaymentMethodModal from "components/Payment/Stripe/PaymentMethod/Modal";
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
  getDefaultPath
} from "utils/coreui/constants";

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
const DefaultLayout = React.lazy(() =>
  import("components/Shared/Layout/Default")
);
const NoneLayout = React.lazy(() => import("components/Shared/Layout/None"));
const Login = React.lazy(() => import("views/Account/Login"));
const Logout = React.lazy(() => import("views/Account/Logout"));
const Register = React.lazy(() => import("views/Account/Register"));

const App: FunctionComponent = () => (
  <>
    <StripePaymentMethodModal.Create />
    <StripePaymentMethodModal.Update />
    <StripePaymentMethodModal.Delete />
    <UserTransactionModal.Create />
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

export default App;
