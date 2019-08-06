import React from 'react';
import ReactDOM from 'react-dom';
import { Route, Switch } from 'react-router-dom';
import { Provider } from 'react-redux';
import { OidcProvider } from 'redux-oidc';
import { ConnectedRouter as Router } from 'connected-react-router';

import { PersistGate } from 'redux-persist/integration/react';

import * as serviceWorker from './serviceWorker';
import { history } from './store/middlewares/routerMiddleware';
import configureStore from './store/configureStore';
import userManager from '../src/utils/userManager';

import Callback from './screens/Shared/Callback';
import App from './components/App';
import Spinner from './components/Shared/Spinner';

const initialState = {};

const { store, persistor } = configureStore(initialState);

ReactDOM.render(
  <Provider store={store}>
    <OidcProvider userManager={userManager} store={store}>
      <PersistGate loading={<Spinner />} persistor={persistor}>
        <Router history={history}>
          <Switch>
            <Route exact path="/callback" component={Callback} />
            <Route path="/" component={App} />
          </Switch>
        </Router>
      </PersistGate>
    </OidcProvider>
  </Provider>,
  document.getElementById('root')
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
