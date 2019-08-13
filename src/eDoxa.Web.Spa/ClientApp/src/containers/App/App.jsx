import React, { Component, Suspense } from "react";
import { Route, Switch } from "react-router-dom";
import { ConnectedRouter as Router } from "connected-react-router";

import { history } from "../../utils/history";

import Loading from "../Shared/Loading";

import "./App.scss";

// Containers
const Callback = React.lazy(() => import("../Layout/Default/Callback"));
const TermsOfServices = React.lazy(() => import("../../pages/TermsOfServices/TermsOfServices"));
const FAQ = React.lazy(() => import("../../pages/FAQ/FAQ"));
const Page404 = React.lazy(() => import("../../pages/Page404/Page404"));
const Page500 = React.lazy(() => import("../../pages/Page500/Page500"));
const Layout = React.lazy(() => import("../Layout/Default"));

class App extends Component {
  render() {
    return (
      <Router history={history}>
        <Suspense fallback={<Loading />}>
          <Switch>
            <Route exact path="/callback" name="Callback" render={props => <Callback {...props} />} />
            <Route exact path="/terms-of-services" name="Terms of Services" render={props => <TermsOfServices {...props} />} />
            <Route exact path="/f-a-q" name="F.A.Q." render={props => <FAQ {...props} />} />
            <Route exact path="/404" name="Page 404" render={props => <Page404 {...props} />} />
            <Route exact path="/500" name="Page 500" render={props => <Page500 {...props} />} />
            <Route path="/" name="Home" render={props => <Layout {...props} />} />
          </Switch>
        </Suspense>
      </Router>
    );
  }
}

export default App;
