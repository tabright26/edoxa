import React from "react";
import { CardElement, injectStripe } from "react-stripe-elements";
import { reduxForm } from "redux-form";
import { CREATE_CARD_FORM } from "forms";

class CreateStripeCreditCardForm extends React.Component {
  handleSubmit = ev => {
    ev.preventDefault();

    var data = {};

    if (this.props.stripe) {
      this.props.stripe.createToken({ type: "card", name: "Jenny Rosen" }).then(payload => (data = payload));
    } else {
      console.log("Stripe.js hasn't loaded yet.");
    }

    if (data) {
      return "tok_visa";
    }
  };

  render() {
    return (
      <form onSubmit={this.handleSubmit} className="stripeElements">
        <label>Card details</label>
        <CardElement />
        <button>Add</button>
      </form>
    );
  }
}

export default reduxForm({ form: CREATE_CARD_FORM })(injectStripe(CreateStripeCreditCardForm));
