import { connect } from "react-redux";
import { injectStripe } from "react-stripe-elements";
import { loadStripePaymentMethods, attachStripePaymentMethod } from "store/root/payment/stripe/paymentMethods/actions";
import Create from "./Create";
import { compose } from "recompose";

const mapDispatchToProps = (dispatch: any, ownProps: any) => {
  return {
    createStripePaymentMethod: () =>
      ownProps.stripe.createPaymentMethod(ownProps.type).then(result => {
        if (result.paymentMethod) {
          return dispatch(attachStripePaymentMethod(result.paymentMethod.id)).then(() => dispatch(loadStripePaymentMethods(ownProps.type)));
        } else {
          return Promise.reject(result.error);
        }
      })
  };
};

const enhance = compose<any, any>(
  injectStripe,
  connect(
    null,
    mapDispatchToProps
  )
);

export default enhance(Create);
