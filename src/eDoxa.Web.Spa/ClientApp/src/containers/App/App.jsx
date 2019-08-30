import React, { Suspense } from "react";
import { Route, Switch } from "react-router-dom";
import { ConnectedRouter as Router } from "connected-react-router";

import { history } from "../../utils/history";

import Loading from "../../components/Loading";

import "./App.scss";

import ArenaChallengeParticipantMatchScoreModal from "../../modals/Arena/Challenge/Participant/Match/Score";

// Containers
const Callback = React.lazy(() => import("../Layout/Default/Callback"));

const PageTermsOfServices = React.lazy(() => import("../../components/Page/TermsOfServices"));
const PageFAQ = React.lazy(() => import("../../components/Page/FAQ"));
const PageSecurityEmailConfirm = React.lazy(() => import("../../components/Page/Security/Email/Comfirm"));
const PageSecurityPasswordReset = React.lazy(() => import("../../components/Page/Security/Password/Reset"));
const PageSecurityPasswordForgot = React.lazy(() => import("../../components/Page/Security/Password/Forgot"));
const PageError401 = React.lazy(() => import("../../components/Page/Error/401"));
const PageError403 = React.lazy(() => import("../../components/Page/Error/403"));
const PageError404 = React.lazy(() => import("../../components/Page/Error/404"));
const PageError500 = React.lazy(() => import("../../components/Page/Error/500"));

const Layout = React.lazy(() => import("../Layout/Default"));

const App = () => (
  <>
    <ArenaChallengeParticipantMatchScoreModal.Details />
    <Router history={history}>
      <Suspense fallback={<Loading.Default />}>
        <Switch>
          <Route exact path="/callback" name="Callback" render={props => <Callback {...props} />} />
          <Route exact path="/terms-of-services" name="Terms of Services" render={props => <PageTermsOfServices {...props} />} />
          <Route exact path="/faq" name="F. A. Q." render={props => <PageFAQ {...props} />} />
          <Route exact path="/security/email/confirm" name="Confirm Email" render={props => <PageSecurityEmailConfirm {...props} />} />
          <Route exact path="/security/password/forgot" name="Forgot Password" render={props => <PageSecurityPasswordForgot {...props} />} />
          <Route exact path="/security/password/reset" name="Reset Password" render={props => <PageSecurityPasswordReset {...props} />} />
          <Route exact path="/errors/401" name="Error 401" render={props => <PageError401 {...props} />} />
          <Route exact path="/errors/403" name="Error 403" render={props => <PageError403 {...props} />} />
          <Route exact path="/errors/404" name="Error 404" render={props => <PageError404 {...props} />} />
          <Route exact path="/errors/500" name="Error 500" render={props => <PageError500 {...props} />} />
          <Route path="/" name="Home" render={props => <Layout {...props} />} />
        </Switch>
      </Suspense>
    </Router>
  </>
);

export default App;
