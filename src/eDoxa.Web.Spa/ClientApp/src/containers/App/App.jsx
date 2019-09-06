import React, { Suspense } from "react";
import { Route, Switch } from "react-router-dom";
import { ConnectedRouter as Router } from "connected-react-router";

import { history } from "../../utils/history";

import Loading from "../../components/Loading";

import "./App.scss";

import ArenaChallengeParticipantMatchScoreModal from "../../modals/Arena/Challenge/Participant/Match/Score";

// Containers
const Callback = React.lazy(() => import("../../components/Shared/Callback"));

const PageError401 = React.lazy(() => import("../../components/Page/Error/401"));
const PageError403 = React.lazy(() => import("../../components/Page/Error/403"));
const PageError404 = React.lazy(() => import("../../components/Page/Error/404"));
const PageError500 = React.lazy(() => import("../../components/Page/Error/500"));

const DefaultLayout = React.lazy(() => import("../Layout/Default"));
const PartialLayout = React.lazy(() => import("../Layout/Partial"));
const NoneLayout = React.lazy(() => import("../Layout/None"));

const App = () => (
  <>
    <ArenaChallengeParticipantMatchScoreModal.Details />
    <Router history={history}>
      <Suspense fallback={<Loading.Default />}>
        <Switch>
          <Route exact path="/callback" name="Callback" render={props => <Callback {...props} />} />
          <Route exact path="/errors/401" name="Error 401" render={props => <PageError401 {...props} />} />
          <Route exact path="/errors/403" name="Error 403" render={props => <PageError403 {...props} />} />
          <Route exact path="/errors/404" name="Error 404" render={props => <PageError404 {...props} />} />
          <Route exact path="/errors/500" name="Error 500" render={props => <PageError500 {...props} />} />
          <Route path="/information/" name="Home" render={props => <PartialLayout {...props} />} />
          <Route path="/security/" name="Home" render={props => <NoneLayout {...props} />} />
          <Route path="/" name="Home" render={props => <DefaultLayout {...props} />} />
        </Switch>
      </Suspense>
    </Router>
  </>
);

export default App;
