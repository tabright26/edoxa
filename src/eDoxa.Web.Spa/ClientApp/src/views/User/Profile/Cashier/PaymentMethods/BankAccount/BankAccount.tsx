import React, { useState, FunctionComponent } from "react";
import { faEdit } from "@fortawesome/free-solid-svg-icons";
import { Card, CardBody, CardHeader } from "reactstrap";
import { withStripeBankAccount } from "store/root/payment/stripe/bankAccount/container";
import BankAccountForm from "forms/Payment/Stripe/BankAccount";
import { compose } from "recompose";
import Button from "components/Shared/Button";
import Loading from "components/Shared/Loading";

const BankAccount: FunctionComponent<any> = ({ className, bankAccount: { data, loading, error }, hasBankAccount }) => {
  const [buttonDisabled, setButtonDisabled] = useState(false);
  return (
    <Card className={`card-accent-primary ${className}`}>
      <CardHeader className="d-flex">
        <strong className="text-uppercase my-auto">BANK ACCOUNT</strong>
        <Button.Link className="p-0 ml-auto my-auto" icon={faEdit} disabled={buttonDisabled} onClick={() => setButtonDisabled(true)}>
          UPDATE
        </Button.Link>
      </CardHeader>
      <CardBody>
        {loading ? (
          <Loading />
        ) : (
          <dl className="row mb-0">
            <dd className="col-sm-3 text-muted mb-0">Bank account</dd>
            <dd className="col-sm-5 mb-0">{buttonDisabled || !hasBankAccount ? <BankAccountForm.Update handleCancel={() => setButtonDisabled(false)} /> : <span>XXXXX-{data.last4}</span>}</dd>
          </dl>
        )}
      </CardBody>
    </Card>
  );
};

const enhance = compose<any, any>(withStripeBankAccount);

export default enhance(BankAccount);
