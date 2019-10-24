import React, { FunctionComponent } from "react";
import { Card, CardHeader, CardBody } from "reactstrap";
import { faEdit, faTimes } from "@fortawesome/free-solid-svg-icons";
import { withStripePaymentMethods } from "store/root/payment/stripe/paymentMethods/container";
import { STRIPE_CARD_TYPE } from "types";
import CardBrandIcon from "components/Payment/Stripe/PaymentMethod/Card/Icon";
import CardExpiration from "components/Payment/Stripe/PaymentMethod/Card/Expiration";
import { faPlus } from "@fortawesome/free-solid-svg-icons";
import Button from "components/Shared/Button";
import { withModals } from "utils/modal/container";
import { compose } from "recompose";
import Loading from "components/Shared/Loading";

const StripeCardItem: FunctionComponent<any> = ({ hasMore, modals, paymentMethod }) => {
  return (
    <>
      <dl className={`row ${!hasMore && "mb-0"}`}>
        <dd className="col-sm-4 m-0 d-flex">
          <CardBrandIcon className="my-auto" brand={paymentMethod.card.brand} size="2x" />
          <span className="ml-2 my-auto">{`XXXX XXXX XXXX ${paymentMethod.card.last4}`}</span>
        </dd>
        <dd className="col-sm-2 m-0 d-flex">
          <span className="my-auto mr-2 text-muted">Expiration:</span>
          <CardExpiration className="my-auto" month={paymentMethod.card.expMonth} year={paymentMethod.card.expYear} />
        </dd>
        <dd className="col-sm-6 mb-0 d-flex">
          <Button.Link className="p-0 ml-auto my-auto" icon={faTimes} onClick={() => modals.showDeleteStripePaymentMethodModal(paymentMethod)}>
            REMOVE
          </Button.Link>
          <Button.Link className="p-0 ml-auto my-auto" icon={faEdit} onClick={() => modals.showUpdateStripePaymentMethodModal(paymentMethod)}>
            UPDATE
          </Button.Link>
        </dd>
      </dl>
      {hasMore && <hr className="border-secondary" />}
    </>
  );
};

const StripeCards: FunctionComponent<any> = ({ className, modals, paymentMethods: { data, loading, error } }) => (
  <Card className={`card-accent-primary ${className}`}>
    <CardHeader className="d-flex">
      <strong className="text-uppercase my-auto">CARDS</strong>
      <Button.Link className="p-0 ml-auto my-auto" icon={faPlus} onClick={() => modals.showCreateStripePaymentMethodModal(STRIPE_CARD_TYPE)}>
        ADD A NEW CARD
      </Button.Link>
    </CardHeader>
    <CardBody>{loading ? <Loading /> : data.map((paymentMethod, index) => <StripeCardItem key={index} paymentMethod={paymentMethod} hasMore={data.length !== index + 1} modals={modals} />)}</CardBody>
  </Card>
);

const enhance = compose<any, any>(
  withStripePaymentMethods,
  withModals
);

export default enhance(StripeCards);
