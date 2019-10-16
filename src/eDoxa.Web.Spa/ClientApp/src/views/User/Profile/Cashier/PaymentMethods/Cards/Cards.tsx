import React, { FunctionComponent } from "react";
import { Card, CardHeader, CardBody } from "reactstrap";
import { faEdit, faTimes } from "@fortawesome/free-solid-svg-icons";
import { withStripePaymentMethods } from "store/root/payment/stripe/paymentMethods/container";
import { STRIPE_PAYMENTMETHOD_CARD_TYPE } from "store/root/payment/stripe/paymentMethods/types";
import CardBrandIcon from "components/Payment/Card/BrandIcon";
import CardExpiration from "components/Payment/Card/Expiration";
import { compose } from "recompose";
import { faPlus } from "@fortawesome/free-solid-svg-icons";
import Button from "components/Shared/Override/Button";

const StripeCardItem: FunctionComponent<any> = ({ index, actions, paymentMethod, length }) => {
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
          <Button.Link className="p-0 ml-auto my-auto" icon={faTimes} onClick={() => actions.showDeletePaymentMethodModal(paymentMethod)}>
            REMOVE
          </Button.Link>
          <Button.Link className="p-0 ml-auto my-auto" icon={faEdit} onClick={() => actions.showUpdatePaymentMethodModal(paymentMethod)}>
            UPDATE
          </Button.Link>
        </dd>
      </dl>
      {length !== index ? <hr className="border-secondary" /> : null}
    </>
  );
};

const StripeCards: FunctionComponent<any> = ({ className, actions, paymentMethods: { data, isLoading, error } }) => (
  <Card className={`card-accent-primary ${className}`}>
    <CardHeader className="d-flex">
      <strong className="text-uppercase my-auto">CARDS</strong>
      <Button.Link className="p-0 ml-auto my-auto" icon={faPlus} onClick={() => actions.showCreatePaymentMethodModal()}>
        ADD A NEW CARD
      </Button.Link>
    </CardHeader>
    <CardBody>
      {data.map((paymentMethod, index) => (
        <StripeCardItem key={index} index={index + 1} paymentMethod={paymentMethod} length={data.length} actions={actions} />
      ))}
    </CardBody>
  </Card>
);

const enhance = compose<any, any>(withStripePaymentMethods(STRIPE_PAYMENTMETHOD_CARD_TYPE));

export default enhance(StripeCards);
