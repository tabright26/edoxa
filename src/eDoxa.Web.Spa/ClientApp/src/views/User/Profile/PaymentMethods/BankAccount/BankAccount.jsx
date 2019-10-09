import React, { useState } from "react";
import { Elements } from "react-stripe-elements";
import { Alert, Row, Col, Card, CardBody, CardText, CardTitle } from "reactstrap";
import { connectStripeBankAccounts } from "store/stripe/bankAccounts/container";
import { injectStripe } from "react-stripe-elements";
import { Button } from "reactstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTimes } from "@fortawesome/free-solid-svg-icons";
import StripeBankAccountForm from "forms/Stripe/BankAccount";

let ChangeBankAccount = ({ hasBankAccount, actions, stripe, changeFormHidden, hideChangeForm }) => {
  return (
    <>
      {!hasBankAccount || !changeFormHidden ? (
        <Row>
          <Col md="4">
            <StripeBankAccountForm.Change onSubmit={fields => actions.changeBankAccount(fields, stripe).then(() => hideChangeForm(true))} handleCancel={() => hideChangeForm(true)} />
          </Col>
          <Col md="8">
            <Card>
              <CardBody className="text-light">
                <CardTitle className="text-uppercase">Account Withdrawal Policies</CardTitle>
                <CardText className="text-justify ">
                  Testqeuwh iuehquweh iqwhe iuhwqe iuhwq uehiuwq heiuwqhehwqieq wiheuqw iuehwq iuheqi hweiuhwq. Testqeuwh iuehquweh iqwhe iuhwqe iuhwq uehiuwq heiuwqhehwqieq wiheuqw iuehwq iuheqi
                  hweiuhwq. Testqeuwh iuehquweh iqwhe iuhwqe iuhwq uehiuwq heiuwqhehwqieq wiheuqw iuehwq iuheqi hweiuhwq.
                </CardText>
              </CardBody>
            </Card>
          </Col>
        </Row>
      ) : (
        <p className="mb-0">Testqeuwh iuehquweh iqwhe iuhwqe iuhwq uehiuwq heiuwqhehwqieq wiheuqw iuehwq iuheqi hweiuhwq.</p>
      )}
    </>
  );
};

ChangeBankAccount = injectStripe(ChangeBankAccount);

const CreditCard = ({ bankAccounts: { data, isLoading, error }, hasBankAccount, actions }) => {
  const [changeBankAccountFormHidden, hideBankAccountChangeForm] = useState(true);
  return (
    <Alert color="primary">
      <h5 className="alert-heading d-flex mb-3">
        <span className="my-auto">BANK ACCOUNT</span>
        {hasBankAccount && changeBankAccountFormHidden ? (
          <Button className="my-auto ml-auto px-0" size="sm" color="link" onClick={() => hideBankAccountChangeForm(false)}>
            <FontAwesomeIcon icon={faTimes} /> CHANGE
          </Button>
        ) : null}
      </h5>
      <Elements>
        <ChangeBankAccount actions={actions} hasBankAccount={hasBankAccount} changeFormHidden={changeBankAccountFormHidden} hideChangeForm={hideBankAccountChangeForm} />
      </Elements>
    </Alert>
  );
};

export default connectStripeBankAccounts(CreditCard);
