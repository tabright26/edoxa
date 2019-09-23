import React from "react";
import { Card, CardHeader, CardBody } from "reactstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEdit, faTimes } from "@fortawesome/free-solid-svg-icons";
import connectStripeCards from "store/stripe/cards/connectStripeCards";
import connectStripePaymentMethods from "store/stripe/cards/container";
import CardBrandIcon from "components/Stripe/Card/BrandIcon";
import CardExpiration from "components/Stripe/Card/Expiration";

let StripeCard = ({ index, actions, card, length }) => (
  <>
    <dl className={`row ${length === index ? "mb-0" : null}`}>
      <dd className="col-sm-4 m-0 d-flex">
        <CardBrandIcon className="my-auto" brand={card.card.brand} size="2x" />
        <span className="ml-2 my-auto">{`XXXX XXXX XXXX ${card.card.last4}`}</span>
      </dd>
      <dd className="col-sm-2 m-0 d-flex">
        <span className="my-auto mr-2 text-muted">Expiration:</span>
        <CardExpiration className="my-auto" month={card.card.exp_month} year={card.card.exp_year} />
      </dd>
      <dd className="col-sm-6 mb-0 d-flex">
        <span className="btn-link ml-auto my-auto" onClick={() => actions.showDeletePaymentMethodModal(card)}>
          <small>
            <FontAwesomeIcon icon={faTimes} /> REMOVE
          </small>
        </span>
        <span className="btn-link ml-auto my-auto" onClick={() => actions.showUpdatePaymentMethodModal(card)}>
          <small>
            <FontAwesomeIcon icon={faEdit} /> UPDATE
          </small>
        </span>
      </dd>
    </dl>
    {length !== index ? <hr className="border-secondary" /> : null}
  </>
);

StripeCard = connectStripePaymentMethods(StripeCard);

const StripeCards = ({ className, cards: { data, isLoading, error } }) => (
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
