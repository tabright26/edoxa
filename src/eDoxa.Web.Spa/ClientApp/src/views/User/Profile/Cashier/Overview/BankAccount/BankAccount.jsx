import React from "react";
import { Elements } from "react-stripe-elements";
import { Card, CardBody, CardHeader } from "reactstrap";
import { connectUser } from "store/user/container";
import { connectStripeBankAccount } from "store/stripe/bankAccount/container";
import { injectStripe } from "react-stripe-elements";
import StripeBankAccountForm from "forms/Stripe/BankAccount";

let UpdateStripeBankAccountForm = ({ actions, stripe, user }) => (
  <StripeBankAccountForm.Update onSubmit={fields => actions.updateBankAccount(fields, user.profile.country, stripe).then(() => {})} handleCancel={() => {}} />
);

UpdateStripeBankAccountForm = injectStripe(UpdateStripeBankAccountForm);

const BankAccount = ({ bankAccount: { data, isLoading, error }, hasBankAccount, user, actions }) => (
  <Card className="card-accent-primary my-4">
    <CardHeader>
      <strong>BANK ACCOUNT</strong>
    </CardHeader>
    <CardBody>
      <Elements>
        <UpdateStripeBankAccountForm actions={actions} user={user} />
      </Elements>
    </CardBody>
  </Card>
);

export default connectUser(connectStripeBankAccount(BankAccount));
