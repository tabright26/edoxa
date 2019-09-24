import React, { useState } from "react";
import { Elements } from "react-stripe-elements";
import { Card, CardHeader, CardBody } from "reactstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlus, faEdit, faTimes } from "@fortawesome/free-solid-svg-icons";
import connectBankAccounts from "store/stripe/bankAccounts/container";
import StripeBankAccountForm from "forms/Stripe/BankAccount";
import StripeBankAccountModal from "modals/Stripe/BankAccount";

const StripeCard = ({ index, actions, card, length }) => {
  const [updateFormHidden, hideUpdateForm] = useState(true);
  const [deleteFormHidden, hideDeleteForm] = useState(true);
  return (
    <>
      <dl className={`row ${length === index ? "mb-0" : null}`}>
        <dd className="col-sm-3 m-0 text-muted">{`Credit Card ${index}`}</dd>
        {!updateFormHidden ? (
          <dd className="col-sm-6 m-0">
            <StripeBankAccountForm.Update initialValues={card} onSubmit={fields => actions.updateCard(card.id, fields).then(() => hideUpdateForm(true))} handleCancel={() => hideUpdateForm(true)} />
          </dd>
        ) : (
          <dd className="col-sm-5 m-0">
            {!deleteFormHidden ? <StripeBankAccountForm.Delete onSubmit={() => actions.deleteCard(card.id).then(() => hideDeleteForm(true))} handleCancel={() => hideDeleteForm(true)} /> : null}
          </dd>
        )}
        {deleteFormHidden && updateFormHidden ? (
          <dd className="col-sm-4 mb-0 d-flex">
            <span
              className="btn-link ml-auto"
              onClick={() => {
                hideUpdateForm(true);
                hideDeleteForm(false);
              }}
            >
              <small>
                <FontAwesomeIcon icon={faTimes} /> REMOVE
              </small>
            </span>
            <span
              className="btn-link ml-auto"
              onClick={() => {
                hideDeleteForm(true);
                hideUpdateForm(false);
              }}
            >
              <small>
                <FontAwesomeIcon icon={faEdit} /> UPDATE
              </small>
            </span>
          </dd>
        ) : null}
      </dl>
      {length !== index ? <hr className="border-secondary" /> : null}
    </>
  );
};

const CreditCard = ({ className, bankAccounts: { data, isLoading, error }, actions }) => (
  <Elements>
    <Card className={className}>
      <CardHeader>
        <strong>BANK ACCOUNT</strong>
        <div className="card-header-actions btn-link" onClick={() => actions.showCreateBankAccountModal()}>
          <small>
            <FontAwesomeIcon icon={faPlus} /> ADD A NEW BANK ACCOUNT
          </small>
        </div>
        <StripeBankAccountModal.Create actions={actions} />
      </CardHeader>
      <CardBody>
        {data.map((card, index) => (
          <StripeCard key={index} index={index + 1} actions={actions} card={card} length={data.length} />
        ))}
      </CardBody>
    </Card>
  </Elements>
);

export default connectBankAccounts(CreditCard);
