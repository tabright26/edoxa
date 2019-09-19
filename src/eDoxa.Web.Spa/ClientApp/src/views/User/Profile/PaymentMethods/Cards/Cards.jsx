import React from "react";
import { Card, CardHeader, CardBody } from "reactstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEdit, faTimes } from "@fortawesome/free-solid-svg-icons";
import connectStripeCards from "containers/connectStripeCards";
import connectStripePaymentMethods from "containers/connectStripePaymentMethods";

let StripeCard = ({ index, actions, card, length }) => {
  console.log(card);
  return (
    <>
      <dl className={`row ${length === index ? "mb-0" : null}`}>
        <dd className="col-sm-2 m-0">{card.card.brand}</dd>
        <dd className="col-sm-2 m-0">{`**** ${card.card.last4}`}</dd>
        <dd className="col-sm-2 m-0">
          {card.card.exp_month}/{card.card.exp_year}
        </dd>
        <dd className="col-sm-6 mb-0 d-flex">
          <span className="btn-link ml-auto" onClick={() => actions.showDeletePaymentMethodModal(card)}>
            <small>
              <FontAwesomeIcon icon={faTimes} /> REMOVE
            </small>
          </span>
          <span className="btn-link ml-auto" onClick={() => actions.showUpdatePaymentMethodModal(card)}>
            <small>
              <FontAwesomeIcon icon={faEdit} /> UPDATE
            </small>
          </span>
        </dd>
      </dl>
      {length !== index ? <hr className="border-secondary" /> : null}
    </>
  );
};

StripeCard = connectStripePaymentMethods(StripeCard);

const StripeCards = ({ className, cards: { data, isLoading, error }, actions }) => (
  <Card className={className}>
    <CardHeader>
      <strong>CARDS</strong>
    </CardHeader>
    <CardBody>
      {data.map((card, index) => (
        <StripeCard key={index} index={index + 1} card={card} length={data.length} />
      ))}
    </CardBody>
  </Card>
);

export default connectStripeCards(StripeCards);
