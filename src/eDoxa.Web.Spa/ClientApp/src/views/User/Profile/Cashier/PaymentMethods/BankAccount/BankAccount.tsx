import React, { useState, FunctionComponent } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEdit } from "@fortawesome/free-solid-svg-icons";
import { Alert } from "reactstrap";
import { withtUserProfile } from "store/root/user/container";
import { withStripeBankAccount } from "store/root/payment/bankAccount/container";
import { injectStripe } from "react-stripe-elements";
import BankAccountForm from "forms/Payment/Stripe/BankAccount";
import { compose } from "recompose";
import Button from "components/Shared/Override/Button";

const BankAccount: FunctionComponent<any> = ({ className, bankAccount: { data, loading, error }, hasBankAccount, country, actions, stripe }) => {
  const [buttonDisabled, setButtonDisabled] = useState(false);
  return (
    <Alert color="primary" className={className}>
      <h6 className="d-flex">
        <strong className="text-uppercase my-auto">BANK ACCOUNT</strong>
        <Button.Link className="p-0 ml-auto my-auto" icon={faEdit} disabled={buttonDisabled} onClick={() => setButtonDisabled(true)}>
          UPDATE
        </Button.Link>
      </h6>
      <hr className="mt-0 mb-2" />
      <dl className="row mb-0">
        <dd className="col-sm-3 text-muted mb-0">Bank account</dd>
        <dd className="col-sm-5 mb-0">
          {buttonDisabled || !hasBankAccount ? (
            <BankAccountForm.Update onSubmit={fields => actions.updateBankAccount(fields, country, stripe).then(() => setButtonDisabled(false))} handleCancel={() => setButtonDisabled(false)} />
          ) : (
            <span>XXXXX-{data.last4}</span>
          )}
        </dd>
      </dl>
    </Alert>
  );
};

const enhance = compose<any, any>(
  injectStripe,
  withtUserProfile("country"),
  withStripeBankAccount
);

export default enhance(BankAccount);
