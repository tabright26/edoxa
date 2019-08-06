import React, { Component } from 'react';
import { Route, Switch } from 'react-router-dom';
import PrivateRoute from '../screens/Shared/PrivateRoute';
import NotFoundPage from '../screens/Shared/NotFound';

import Header from './Shared/Header';
import Home from '../screens/Home/Index';
import Footer from './Shared/Footer';

import Challenge from './Arena/Challenge/Details';
import Challenges from './Arena/Challenge/Index';

import AccountOverview from '../screens/Account/Overview';
import TransactionHistory from '../screens/Account/TransactionHistory';
import PaymentMethods from '../screens/Account/PaymentMethods';

import ScreensUserGameIndex from '../screens/User/Game/Index';

import 'bootstrap/dist/css/bootstrap.min.css';
import '../styles/bootstrap-custom.scss';

class App extends Component {
  render() {
    return (
      <main className="mt-5">
        <Header />
        <Switch>
          <Route exact path="/" component={Home} />
          <PrivateRoute exact path="/challenges" component={Challenges} />
          <PrivateRoute
            exact
            path="/challenges/:challengeId"
            component={Challenge}
          />
          <PrivateRoute
            exact
            path="/payment-methods"
            component={PaymentMethods}
          />
          <PrivateRoute
            exact
            path="/transaction-history"
            component={TransactionHistory}
          />
          <PrivateRoute
            exact
            path="/account/overview"
            component={AccountOverview}
          />
          <PrivateRoute
            exact
            path="/arena/games"
            component={ScreensUserGameIndex}
          />
          <Route component={NotFoundPage} />
        </Switch>
        <Footer />
      </main>
    );
  }
}

export default App;
