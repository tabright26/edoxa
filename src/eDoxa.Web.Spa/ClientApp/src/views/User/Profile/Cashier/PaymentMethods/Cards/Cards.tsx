import React, { FunctionComponent } from "react";
import { Card, CardHeader, CardBody } from "reactstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEdit, faTimes } from "@fortawesome/free-solid-svg-icons";
import { connectStripePaymentMethods } from "store/root/payment/paymentMethods/container";
import { CARD_PAYMENTMETHOD_TYPE } from "store/root/payment/paymentMethods/types";
import CardBrandIcon from "components/Payment/Card/BrandIcon";
import CardExpiration from "components/Payment/Card/Expiration";

const StripeCard: FunctionComponent<any> = ({ index, actions, paymentMethod, length }) => {
  return (
    <>
      <dl className={`row ${length === index ? "mb-0" : null}`}>
        <dd className="col-sm-4 m-0 d-flex">
          <CardBrandIcon className="my-auto" brand={paymentMethod.card.brand} size="2x" />
          <span className="ml-2 my-auto">{`XXXX XXXX XXXX ${paymentMethod.card.last4}`}</span>
        </dd>
        <dd className="col-sm-2 m-0 d-flex">
          <span className="my-auto mr-2 text-muted">Expiration:</span>
          <CardExpiration className="my-auto" month={paymentMethod.card.exp_month} year={paymentMethod.card.exp_year} />
        </dd>
        <dd className="col-sm-6 mb-0 d-flex">
          <span className="btn-link ml-auto my-auto" onClick={() => actions.showDeletePaymentMethodModal(paymentMethod)}>
            <small>
              <FontAwesomeIcon icon={faTimes} /> REMOVE
            </small>
          </span>
          <span className="btn-link ml-auto my-auto" onClick={() => actions.showUpdatePaymentMethodModal(paymentMethod)}>
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

const StripeCards: FunctionComponent<any> = ({ className, actions, paymentMethods: { data, isLoading, error } }) => (
  <Card className={className}>
    <CardHeader>
      <strong>CARDS</strong>
    </CardHeader>
    <CardBody>
      {data.map((paymentMethod, index) => (
        <StripeCard key={index} index={index + 1} paymentMethod={paymentMethod} length={data.length} actions={actions} />
      ))}
    </CardBody>
  </Card>
);

export default connectStripePaymentMethods(CARD_PAYMENTMETHOD_TYPE)(StripeCards);
