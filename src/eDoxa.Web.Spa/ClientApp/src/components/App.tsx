import "./App.scss";
import React, { Suspense, FunctionComponent } from "react";
import { Route, Switch, Redirect, RouteComponentProps } from "react-router-dom";
import { ConnectedRouter as Router } from "connected-react-router";
import { history } from "utils/router/config";
import UserAccountModal from "modals/User/Account";
import MatchScoreModal from "modals/Arena/Challenge/Participant/Match/Score";
import Loading from "./Shared/Override/Loading";
import { RouteProps } from "utils/router/types";

const Callback: FunctionComponent<RouteComponentProps> = React.lazy(() => import("./Shared/Callback"));
const ErrorPage401: FunctionComponent<RouteComponentProps> = React.lazy(() => import("./Shared/ErrorPage/401"));
const ErrorPage403: FunctionComponent<RouteComponentProps> = React.lazy(() => import("./Shared/ErrorPage/403"));
const ErrorPage404: FunctionComponent<RouteComponentProps> = React.lazy(() => import("./Shared/ErrorPage/404"));
const ErrorPage500: FunctionComponent<RouteComponentProps> = React.lazy(() => import("./Shared/ErrorPage/500"));
const TermsOfServices: FunctionComponent<RouteComponentProps> = React.lazy(() => import("views/TermsOfServices"));
const FAQ: FunctionComponent<RouteComponentProps> = React.lazy(() => import("views/FAQ"));
const EmailConfirm: FunctionComponent<RouteComponentProps> = React.lazy(() => import("views/User/Email/Comfirm"));
const PasswordForgot: FunctionComponent<RouteComponentProps> = React.lazy(() => import("views/User/Password/Forgot"));
const PasswordReset: FunctionComponent<RouteComponentProps> = React.lazy(() => import("views/User/Password/Reset"));

const DefaultLayout: FunctionComponent<any> = React.lazy(() => import("./Shared/Layout/Default"));
const PartialLayout: FunctionComponent<any> = React.lazy(() => import("./Shared/Layout/Partial"));
const NoneLayout: FunctionComponent<any> = React.lazy(() => import("./Shared/Layout/None"));

const App = () => (
  <>
    <UserAccountModal.Deposit />
    <UserAccountModal.Withdrawal />
    <MatchScoreModal />
    <Router history={history}>
      <Suspense fallback={<Loading />}>
        <Switch>
          <Route<RouteProps> exact path="/callback" name="Callback" render={props => <Callback {...props} />} />
          <Route<RouteProps> exact path="/errors/401" name="Error 401" render={props => <ErrorPage401 {...props} />} />
          <Route<RouteProps> exact path="/errors/403" name="Error 403" render={props => <ErrorPage403 {...props} />} />
          <Route<RouteProps> exact path="/errors/404" name="Error 404" render={props => <ErrorPage404 {...props} />} />
          <Route<RouteProps> exact path="/errors/500" name="Error 500" render={props => <ErrorPage500 {...props} />} />
          <Route<RouteProps>
            exact
            path="/terms-of-services"
            name="Terms of Services"
            render={props => (
              <Suspense fallback={<Loading />}>
                <PartialLayout>
                  <TermsOfServices {...props} />
                </PartialLayout>
              </Suspense>
            )}
          />
          <Route<RouteProps>
            exact
            path="/faq"
            name="FAQ"
            render={props => (
              <Suspense fallback={<Loading />}>
                <PartialLayout>
                  <FAQ {...props} />
                </PartialLayout>
              </Suspense>
            )}
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
          <Route<RouteProps> path="/" name="Home" render={props => <DefaultLayout {...props} />} />
          <Redirect to="/errors/404" />
        </Switch>
      </Suspense>
    </Router>
  </>
);

export default App;
