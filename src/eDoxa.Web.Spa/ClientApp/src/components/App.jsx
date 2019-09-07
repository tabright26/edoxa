import React, { Suspense } from "react";
import { Route, Switch, Redirect } from "react-router-dom";
import { ConnectedRouter as Router } from "connected-react-router";

import { history } from "../utils/history";

import Loading from "./Loading";

import "./App.scss";

import ArenaChallengeParticipantMatchScoreModal from "../modals/Arena/Challenge/Participant/Match/Score";

const Callback = React.lazy(() => import("./Callback"));

const ErrorPage401 = React.lazy(() => import("./ErrorPage/401"));
const ErrorPage403 = React.lazy(() => import("./ErrorPage/403"));
const ErrorPage404 = React.lazy(() => import("./ErrorPage/404"));
const ErrorPage500 = React.lazy(() => import("./ErrorPage/500"));

const DefaultLayout = React.lazy(() => import("./Layout/Default"));
const PartialLayout = React.lazy(() => import("./Layout/Partial"));
const NoneLayout = React.lazy(() => import("./Layout/None"));

const TermsOfServices = React.lazy(() => import("../views/TermsOfServices"));
const FAQ = React.lazy(() => import("../views/FAQ"));
const EmailConfirm = React.lazy(() => import("../views/User/Email/Comfirm"));
const PasswordForgot = React.lazy(() => import("../views/User/Password/Forgot"));
const PasswordReset = React.lazy(() => import("../views/User/Password/Reset"));

const App = () => (
  <>
    <ArenaChallengeParticipantMatchScoreModal.Details />
    <Router history={history}>
      <Suspense fallback={<Loading.Default />}>
        <Switch>
          <Route exact path="/callback" name="Callback" render={props => <Callback {...props} />} />
          <Route exact path="/errors/401" name="Error 401" render={props => <ErrorPage401 {...props} />} />
          <Route exact path="/errors/403" name="Error 403" render={props => <ErrorPage403 {...props} />} />
          <Route exact path="/errors/404" name="Error 404" render={props => <ErrorPage404 {...props} />} />
          <Route exact path="/errors/500" name="Error 500" render={props => <ErrorPage500 {...props} />} />
          <Route
            exact
            path="/terms-of-services"
            name="Terms of Services"
            render={props => (
              <Suspense fallback={<Loading.Default />}>
                <PartialLayout>
                  <TermsOfServices {...props} />
                </PartialLayout>
              </Suspense>
            )}
          />
          <Route
            exact
            path="/faq"
            name="FAQ"
            render={props => (
              <Suspense fallback={<Loading.Default />}>
                <PartialLayout>
                  <FAQ {...props} />
                </PartialLayout>
              </Suspense>
            )}
          />
          <Route
            exact
            path="/email/confirm"
            name="Confirm Email"
            render={props => (
              <Suspense fallback={<Loading.Default />}>
                <NoneLayout>
                  <EmailConfirm {...props} />
                </NoneLayout>
              </Suspense>
            )}
          />
          <Route
            exact
            path="/password/forgot"
            name="Forgot Password"
            render={props => (
              <Suspense fallback={<Loading.Default />}>
                <NoneLayout>
                  <PasswordForgot {...props} />
                </NoneLayout>
              </Suspense>
            )}
          />
          <Route
            exact
            path="/password/reset"
            name="Reset Password"
            render={props => (
              <Suspense fallback={<Loading.Default />}>
                <NoneLayout>
                  <PasswordReset {...props} />
                </NoneLayout>
              </Suspense>
            )}
          />
          <Route path="/" name="Home" render={props => <DefaultLayout {...props} />} />
          <Redirect to="/errors/404" />
        </Switch>
      </Suspense>
    </Router>
  </>
);

export default App;
