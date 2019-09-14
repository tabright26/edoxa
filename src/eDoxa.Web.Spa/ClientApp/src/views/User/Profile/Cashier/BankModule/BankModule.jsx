import React, { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEdit, faPlus, faTimes } from "@fortawesome/free-solid-svg-icons";
import { Container, Row, Col, Card, CardHeader, CardBody } from "reactstrap";

import BankForm from "../../../../../forms/Stripe/Bank";
import StripeBankModal from "../../../../../modals/Stripe/Bank";
import Bank from "../../../../../components/Stripe/Bank";
import withStripeBankHoc from "../../../../../containers/App/Stripe/Bank/withStripeBankHoc";

const BankCard = ({ actions, bank }) => {
  const [updateFormHidden, hideUpdateForm] = useState(true);
  const [deleteFormHidden, hideDeleteForm] = useState(true);
  return (
    <>
      <dl className="row">
        <dd className="col-sm-3 m-0 text-muted">
          Default Bank Account
          <br />
          <small>{bank.id}</small>
        </dd>
        {!updateFormHidden ? (
          <dd className="col-sm-6 m-0">
            <BankForm.Update onSubmit={fields => actions.updateStripeBank(bank.id, fields).then(() => hideUpdateForm(true))} handleCancel={() => hideUpdateForm(true)} />
          </dd>
        ) : (
          <dd className="col-sm-5 m-0">
            <Bank bank={bank} />
            {!deleteFormHidden ? <BankForm.Delete onSubmit={() => actions.removeStripeBank(bank.id).then(() => hideDeleteForm(true))} handleCancel={() => hideDeleteForm(true)} /> : null}
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
        <small>{bank.id}</small>
      </dl>
    </>
  );
};

const BankModule = ({ className, actions, bank, stripeCustomerId }) => (
  <Container>
    <Row>
      <Col>
        <h5 className="my-4">WITHDRAWAL METHODS</h5>
        <Card className={className}>
          <CardHeader>
            <strong>Bank Account</strong> : <small>Stripe Customer ID: {stripeCustomerId}</small>
            <div className="card-header-actions btn-link" onClick={() => actions.showCreateBankModal()}>
              <small>
                <FontAwesomeIcon icon={faPlus} /> ADD A NEW BANK ACCOUNT
              </small>
            </div>
            <StripeBankModal.Create actions={actions} />
          </CardHeader>
          <CardBody>{bank.id ? <BankCard actions={actions} bank={bank} /> : null}</CardBody>
        </Card>
      </Col>
    </Row>
  </Container>
);

export default withStripeBankHoc(BankModule);
