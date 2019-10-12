import React, { useState } from "react";
import { Elements } from "react-stripe-elements";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEdit } from "@fortawesome/free-solid-svg-icons";
import { Alert } from "reactstrap";
import { connectUser } from "store/user/container";
import { connectStripeBankAccount } from "store/stripe/bankAccount/container";
import { injectStripe } from "react-stripe-elements";
import StripeBankAccountForm from "forms/Stripe/BankAccount";

let UpdateStripeBankAccountForm = ({ actions, stripe, user, setFormHidden }) => (
  <StripeBankAccountForm.Update onSubmit={fields => actions.updateBankAccount(fields, user.profile.country, stripe).then(() => {})} handleCancel={() => setFormHidden(true)} />
);

UpdateStripeBankAccountForm = injectStripe(UpdateStripeBankAccountForm);

const BankAccount = ({ bankAccount: { data, isLoading, error }, hasBankAccount, user, actions }) => {
  const [isFormHidden, setFormHidden] = useState(true);
  console.log(data);
  console.log(hasBankAccount);
  return (
    <Alert color="primary">
      <h5>
        <strong>BANK ACCOUNT</strong>
        {isFormHidden ? (
          <div className="card-header-actions btn-link" onClick={() => setFormHidden(false)}>
            <small>
              <FontAwesomeIcon icon={faEdit} /> UPDATE
            </small>
          </div>
        ) : null}
      </h5>
      <hr className="mt-0" />
      <dl className="row mb-0">
        <dd className="col-sm-3 text-muted mb-0">Bank account</dd>
        <dd className="col-sm-5 mb-0">
          {!isFormHidden || !hasBankAccount ? (
            <Elements>
              <UpdateStripeBankAccountForm actions={actions} user={user} setFormHidden={setFormHidden} />
            </Elements>
          ) : (
            <span>{data.last4}</span>
          )}
        </dd>
      </dl>
    </Alert>
  );
};

export default connectUser(connectStripeBankAccount(BankAccount));
