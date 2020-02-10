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
import ReduxToastr from "react-redux-toastr";
import { StripeProvider } from "react-stripe-elements";
import { LocalizeProvider } from "react-localize-redux";
import { initialize } from "utils/localize/initialize";
import { OidcProvider, loadUser } from "redux-oidc";
import { userManager } from "utils/oidc/UserManager";
import MessengerCustomerChat from "react-messenger-customer-chat";
import { loadUserTransactionHistory } from "store/actions/cashier";
import {
  loadIdentityStaticOptions,
  loadCashierStaticOptions,
  loadChallengesStaticOptions,
  loadGamesStaticOptions
} from "store/actions/static";

loadUser(store, userManager).then(user => {
  if (user) {
    store.dispatch<any>(loadUserTransactionHistory());
  }
});
store.dispatch<any>(loadIdentityStaticOptions());
store.dispatch<any>(loadCashierStaticOptions());
store.dispatch<any>(loadChallengesStaticOptions());
store.dispatch<any>(loadGamesStaticOptions());

ReactDOM.render(
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
    {process.env.NODE_ENV === "production" && (
      <MessengerCustomerChat
        pageId={process.env.REACT_APP_FACEBOOK_MCC_PAGE_ID}
        appId={process.env.REACT_APP_FACEBOOK_MCC_APP_ID}
        version="5.0"
        xfbml
        themeColor="#ff6f00"
      />
    )}
    <ReduxToastr
      timeOut={7500}
      newestOnTop={false}
      preventDuplicates
      position="bottom-right"
      transitionIn="fadeIn"
      transitionOut="fadeOut"
      progressBar={false}
      closeOnToastrClick
    />
  </Provider>,
  document.getElementById("root")
);

// // If you want your app to work offline and load faster, you can change
// // unregister() to register() below. Note this comes with some pitfalls.
// // Learn more about service workers: http://bit.ly/CRA-PWA
serviceWorker.unregister();
