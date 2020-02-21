// import 'react-app-polyfill/ie9'; // For IE 9-11 support
import "react-app-polyfill/ie11"; // For IE 11 support
import "react-app-polyfill/stable";
import "./polyfill";
import React from "react";
import ReactDOM from "react-dom";
import App from "./components/App";
import * as serviceWorker from "./serviceWorker";
import { Provider } from "react-redux";
import store from "./store";
import { StripeProvider } from "react-stripe-elements";
import { LocalizeProvider } from "react-localize-redux";
import { initialize } from "utils/localize/initialize";
import { OidcProvider, loadUser } from "redux-oidc";
import { userManager } from "utils/oidc/UserManager";
import { loadUserTransactionHistory } from "store/actions/cashier";
import { loadStripePaymentMethods } from "store/actions/payment";
import { MessengerCustomerChat } from "utils/facebook/MessengerCustomerChat";
import { ToastrProvider } from "utils/toastr/Provider";
import { CookiesProvider } from "react-cookie";

loadUser(store, userManager).then(user => {
  if (user) {
    store.dispatch<any>(loadUserTransactionHistory());
    store.dispatch<any>(loadStripePaymentMethods());
  }
});

ReactDOM.render(
  <CookiesProvider>
    <Provider store={store}>
      <OidcProvider store={store} userManager={userManager}>
        <LocalizeProvider store={store} initialize={initialize}>
          <StripeProvider
            apiKey={process.env.REACT_APP_STRIPE_APIKEYS_PUBLISHABLEKEY}
          >
            <App />
          </StripeProvider>
        </LocalizeProvider>
      </OidcProvider>
      <ToastrProvider />
      <MessengerCustomerChat />
    </Provider>
  </CookiesProvider>,
  document.getElementById("root")
);

// // If you want your app to work offline and load faster, you can change
// // unregister() to register() below. Note this comes with some pitfalls.
// // Learn more about service workers: http://bit.ly/CRA-PWA
serviceWorker.unregister();
