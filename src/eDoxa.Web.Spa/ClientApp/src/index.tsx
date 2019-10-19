// import 'react-app-polyfill/ie9'; // For IE 9-11 support
import "react-app-polyfill/ie11"; // For IE 11 support
import "react-app-polyfill/stable";
import "./polyfill";
import React from "react";
import ReactDOM from "react-dom";
import App from "./components/App";
import * as serviceWorker from "./serviceWorker";

import { Provider } from "react-redux";
import { configureStore } from "./store";
import { OidcProvider } from "redux-oidc";
import userManager from "utils/oidc/manager";
import ReduxToastr from "react-redux-toastr";
import { StripeProvider } from "react-stripe-elements";
import { LocalizeProvider } from "react-localize-redux";
import { config } from "utils/localize/config";

const initialState: any = {};

const store = configureStore(initialState);

ReactDOM.render(
  <Provider store={store}>
    <OidcProvider store={store} userManager={userManager}>
      <LocalizeProvider store={store} initialize={config}>
        <StripeProvider apiKey={process.env.REACT_APP_STRIPE_APIKEYS_PUBLISHABLEKEY}>
          <App />
        </StripeProvider>
      </LocalizeProvider>
    </OidcProvider>
    <ReduxToastr timeOut={4000} newestOnTop={false} preventDuplicates position="bottom-right" transitionIn="fadeIn" transitionOut="fadeOut" progressBar closeOnToastrClick />
  </Provider>,
  document.getElementById("root")
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: http://bit.ly/CRA-PWA
serviceWorker.unregister();
