import React, { useState, FunctionComponent } from "react";
import { faEdit } from "@fortawesome/free-solid-svg-icons";
import { Card, CardBody, CardHeader, Button } from "reactstrap";
import { withStripeBankAccount } from "store/root/payment/stripe/bankAccount/container";
import BankAccountForm from "components/Payment/Stripe/BankAccount/Form";
import { compose } from "recompose";
import { Loading } from "components/Shared/Loading";
import {
  withUserProfileCountry,
  HocUserProfileCountryStateProps
} from "utils/oidc/containers";
import { StripeBankAccountState } from "store/root/payment/stripe/bankAccount/types";
import { Elements } from "react-stripe-elements";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

type InnerProps = HocUserProfileCountryStateProps & {
  bankAccount: StripeBankAccountState;
  hasBankAccount: boolean;
};

type OutterProps = {
  className?: string;
};

type Props = InnerProps & OutterProps;

const Panel: FunctionComponent<Props> = ({
  className = null,
  country,
  bankAccount: { data, loading },
  hasBankAccount
}) => {
  const [buttonDisabled, setButtonDisabled] = useState(false);
  const disabled = buttonDisabled || !hasBankAccount;
  return (
    <Card className={`card-accent-primary ${className}`}>
      <CardHeader className="d-flex">
        <strong className="text-uppercase my-auto">BANK ACCOUNT</strong>
        <Button
          className="p-0 ml-auto my-auto"
          disabled={disabled}
          color="link"
          size="sm"
          onClick={() => setButtonDisabled(true)}
        >
          <small className="text-uppercase">
            <FontAwesomeIcon icon={faEdit} /> UPDATE
          </small>
        </Button>
      </CardHeader>
      <CardBody>
        {loading ? (
          <Loading />
        ) : (
          <dl className="row mb-0">
            <dd className="col-sm-3 text-muted mb-0">Bank account</dd>
            <dd className="col-sm-5 mb-0">
              {disabled && (
                <Elements>
                  <BankAccountForm.Update
                    country={country}
                    handleCancel={() => setButtonDisabled(false)}
                  />
                </Elements>
              )}
              {!disabled && <span>XXXXX-{data.last4}</span>}
            </dd>
          </dl>
        )}
      </CardBody>
    </Card>
  );
};

const enhance = compose<InnerProps, OutterProps>(
  withUserProfileCountry,
  withStripeBankAccount
);

export default enhance(Panel);
