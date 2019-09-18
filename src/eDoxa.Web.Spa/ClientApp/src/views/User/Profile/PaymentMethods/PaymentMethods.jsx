import React, { Fragment, Suspense } from "react";
import Loading from "components/Shared/Loading";

import CreditCardModule from "../Cashier/CreditCardModule";
import BankModule from "../Cashier/BankModule";
import { StripeProvider, CardNumberElement, CardExpiryElement, CardCVCElement, Elements, injectStripe } from "react-stripe-elements";
// const Cashier = ({ className, actions }) => (
//   <Fragment>
//     <Suspense fallback={<Loading.Default />}>
//       <CreditCardModule />
//     </Suspense>
//     <Suspense fallback={<Loading.Default />}>
//       <BankModule />
//     </Suspense>
//   </Fragment>
// );

const createOptions = () => {
  return {
    style: {
      base: {
        fontSize: "16px",
        color: "#424770",
        letterSpacing: "0.025em",
        "::placeholder": {
          color: "#aab7c4"
        }
      },
      invalid: {
        color: "#c23d4b"
      }
    }
  };
};

class CheckoutForm extends React.Component {
  state = {
    errorMessage: ""
  };

  handleChange = ({ error }) => {
    if (error) {
      this.setState({ errorMessage: error.message });
    }
  };

  handleSubmit = evt => {
    evt.preventDefault();
    if (this.props.stripe) {
      this.props.stripe.createToken().then(this.props.handleResult);
    } else {
      console.log("Stripe.js hasn't loaded yet.");
    }
  };

  render() {
    return (
      <form onSubmit={this.handleSubmit.bind(this)}>
        <div className="split-form">
          <label>
            Card number
            <CardNumberElement {...createOptions()} onChange={this.handleChange} />
          </label>
          <label>
            Expiration date
            <CardExpiryElement {...createOptions()} onChange={this.handleChange} />
          </label>
        </div>
        <div className="split-form">
          <label>
            CVC
            <CardCVCElement {...createOptions()} onChange={this.handleChange} />
          </label>
        </div>
        <div className="error" role="alert">
          {this.state.errorMessage}
        </div>
        <button>Pay</button>
      </form>
    );
  }
}

const Form = injectStripe(CheckoutForm);

const PaymentMethods = () => (
  <StripeProvider apiKey="pk_test_2iUjQQxve1uP7BJOxs5SwQvj">
    <Elements>
      <Form />
    </Elements>
  </StripeProvider>
);

export default PaymentMethods;
