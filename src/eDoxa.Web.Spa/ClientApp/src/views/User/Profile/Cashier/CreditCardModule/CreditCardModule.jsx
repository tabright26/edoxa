import React, { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEdit, faPlus, faTimes } from "@fortawesome/free-solid-svg-icons";
import { Container, Row, Col, Card, CardHeader, CardBody } from "reactstrap";

import CreditCardForm from "forms/Stripe/Card";
import StripeCardModal from "modals/Stripe/Card";
import CreditCard from "components/Stripe/Card";
import withStripeCardHoc from "containers/connectStripeCards";

const CreditCardCard = ({ index, actions, card, length }) => {
  const [updateFormHidden, hideUpdateForm] = useState(true);
  const [deleteFormHidden, hideDeleteForm] = useState(true);
  return (
    <>
      <dl className={`row ${length === index ? "mb-0" : null}`}>
        <dd className="col-sm-3 m-0 text-muted">
          {`${index === 1 ? "Default Card" : "Card #".concat(index)}`}
          <br />
          <small>{card.id}</small>
        </dd>
        {!updateFormHidden ? (
          <dd className="col-sm-6 m-0">
            <CreditCardForm.Update onSubmit={fields => actions.updateStripeCreditCard(card.id, fields).then(() => hideUpdateForm(true))} handleCancel={() => hideUpdateForm(true)} />
          </dd>
        ) : (
          <dd className="col-sm-5 m-0">
            <CreditCard card={card} />
            {!deleteFormHidden ? <CreditCardForm.Delete onSubmit={() => actions.removeStripeCreditCard(card.id).then(() => hideDeleteForm(true))} handleCancel={() => hideDeleteForm(true)} /> : null}
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

const CreditCardModule = ({ className, actions, cards, stripeCustomerId }) => (
  <Container>
    <Row>
      <Col>
        <h5 className="my-4">PAYMENT METHODS</h5>
        <Card className={className}>
          <CardHeader>
            <strong>Credit Cards</strong> : <small>Stripe Customer ID: {stripeCustomerId}</small>
            <div className="card-header-actions btn-link" onClick={() => actions.showCreateCreditCardModal()}>
              <small>
                <FontAwesomeIcon icon={faPlus} /> ADD A NEW CREDIT CARD
              </small>
            </div>
            <StripeCardModal.Create actions={actions} />
          </CardHeader>
          <CardBody>
            {cards.map((card, index) => (
              <CreditCardCard key={index} index={index + 1} actions={actions} card={card} length={card.length} />
            ))}
          </CardBody>
        </Card>
      </Col>
    </Row>
  </Container>
);

export default withStripeCardHoc(CreditCardModule);
