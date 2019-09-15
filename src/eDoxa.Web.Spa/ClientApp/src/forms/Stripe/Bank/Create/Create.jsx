import "./stripeElements.css";
import React from "react";
import { IbanElement, injectStripe } from "react-stripe-elements";
import { reduxForm } from "redux-form";
import { CREATE_BANK_FORM } from "forms";

class CreateStripeBankForm extends React.Component {
  handleSubmit = ev => {
    ev.preventDefault();

    var data = {};

    if (this.props.stripe) {
      this.props.stripe
        .createSource({
          type: "sepa_debit",
          currency: "usd",
          owner: {
            name: ev.target.name.value,
            email: ev.target.email.value
          },
          mandate: {
            notification_method: "email"
          }
        })
        .then(payload => {
          console.log(data);
          data = payload;
        });
    } else {
      console.log("Stripe.js hasn't loaded yet.");
    }

    console.log(data);

    if (data) {
      return data;
    }
  };

  render() {
    return (
      <form onSubmit={this.handleSubmit} className="stripeElements">
        <label>Bank details</label>
        <br />
        <label>
          Name
          <input name="name" type="text" placeholder="Jane Doe" required />
        </label>
        <br />
        <label>
          Email
          <input name="email" type="email" placeholder="jane.doe@example.com" required />
        </label>
        <br />
        <IbanElement supportedCountries={["SEPA"]} />
        <button>Add</button>
      </form>
    );
  }
}

export default reduxForm({ form: CREATE_BANK_FORM })(injectStripe(CreateStripeBankForm));
