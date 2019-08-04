import React, { Component } from 'react';
import { Route, Switch } from 'react-router-dom';
import PrivateRoute from '../utils/PrivateRoute';
import NotFoundPage from '../utils/NotFound';

import Header from './Shared/Header';
import Home from '../screens/Home/Index';
import Footer from './Shared/Footer';

import Challenge from './challenge/Challenge';
import Challenges from './challenge/Challenges';

import AccountOverview from './cashier/AccountOverview';
import TransactionHistory from './cashier/TransactionHistory';
import PaymentMethods from './cashier/PaymentMethods';

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
