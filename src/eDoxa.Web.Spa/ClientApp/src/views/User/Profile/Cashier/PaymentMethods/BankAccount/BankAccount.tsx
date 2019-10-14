import React, { useState, FunctionComponent } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEdit } from "@fortawesome/free-solid-svg-icons";
import { Alert } from "reactstrap";
import { withtUser } from "store/root/user/container";
import { withStripeBankAccount } from "store/root/payment/bankAccount/container";
import { injectStripe } from "react-stripe-elements";
import BankAccountForm from "forms/Payment/Stripe/BankAccount";
import { compose } from "recompose";

const BankAccount: FunctionComponent<any> = ({ bankAccount: { data, isLoading, error }, hasBankAccount, user, actions, stripe }) => {
  const [isFormHidden, setFormHidden] = useState(true);
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
        <dd className="col-sm-5 mb-0">{!isFormHidden || !hasBankAccount ?<BankAccountForm.Update onSubmit={fields => actions.updateBankAccount(fields, user.profile.country, stripe).then(() => {})} handleCancel={() => setFormHidden(true)} /> : <span>{data.last4}</span>}</dd>
      </dl>
    </Alert>
  );
};

const enhance = compose<any, any>(
  injectStripe,
  withtUser,
  withStripeBankAccount
);

export default enhance(BankAccount);
