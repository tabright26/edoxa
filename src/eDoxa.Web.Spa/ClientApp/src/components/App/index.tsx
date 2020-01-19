import "./index.scss";
import React, { Suspense, FunctionComponent } from "react";
import { Route, Switch, Redirect, RouteComponentProps } from "react-router-dom";
import { ConnectedRouter as Router } from "connected-react-router";
import { history } from "utils/router/history";
import UserTransactionModal from "components/User/Transaction/Modal";
import CustomModal from "components/Challenge/Participant/Match/Modal/Score";
import { Loading } from "components/Shared/Loading";
import { RouteProps } from "utils/router/types";
import StripePaymentMethodModal from "components/Payment/Stripe/PaymentMethod/Modal";
import { Elements } from "react-stripe-elements";
import { ApplicationPaths } from "utils/oidc/ApiAuthorizationConstants";
import {
  getError401Path,
  getError403Path,
  getError404Path,
  getError500Path,
  getHomePath,
  getEmailConfirmPath,
  getPasswordForgotPath,
  getPasswordResetPath
} from "utils/coreui/constants";

const ApiAuthorizationRoutes: FunctionComponent<RouteComponentProps> = React.lazy(
  () => import("utils/oidc/ApiAuthorizationRoutes")
);
const ErrorPage401: FunctionComponent<RouteComponentProps> = React.lazy(() =>
  import("views/Errors/401")
);
const ErrorPage403: FunctionComponent<RouteComponentProps> = React.lazy(() =>
  import("views/Errors/403")
);
const ErrorPage404: FunctionComponent<RouteComponentProps> = React.lazy(() =>
  import("views/Errors/404")
);
const ErrorPage500: FunctionComponent<RouteComponentProps> = React.lazy(() =>
  import("views/Errors/500")
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
  import("components/Shared/Layout/Default")
);
const NoneLayout: FunctionComponent<any> = React.lazy(() =>
  import("components/Shared/Layout/None")
);

const App = () => (
  <>
    <Elements>
      <StripePaymentMethodModal.Create />
    </Elements>
    <StripePaymentMethodModal.Update />
    <StripePaymentMethodModal.Delete />
    <UserTransactionModal.Create />
    <CustomModal />
    <Router history={history}>
      <Suspense fallback={<Loading />}>
        <Switch>
          <Route<any>
            path={ApplicationPaths.ApiAuthorizationPrefix}
            component={props => <ApiAuthorizationRoutes {...props} />}
          />
          <Route<RouteProps>
            exact
            path={getError401Path()}
            name="Error 401"
            render={props => <ErrorPage401 {...props} />}
          />
          <Route<RouteProps>
            exact
            path={getError403Path()}
            name="Error 403"
            render={props => <ErrorPage403 {...props} />}
          />
          <Route<RouteProps>
            exact
            path={getError404Path()}
            name="Error 404"
            render={props => <ErrorPage404 {...props} />}
          />
          <Route<RouteProps>
            exact
            path={getError500Path()}
            name="Error 500"
            render={props => <ErrorPage500 {...props} />}
          />
          <Route<RouteProps>
            exact
            path={getEmailConfirmPath()}
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
            path={getPasswordForgotPath()}
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
            path={getPasswordResetPath()}
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
            path={getHomePath()}
            name="Home"
            render={props => <DefaultLayout {...props} />}
          />
          <Redirect to={getError404Path()} />
        </Switch>
      </Suspense>
    </Router>
  </>
);

export default App;
