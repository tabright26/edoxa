import { connect } from "react-redux";
import { loadStripePaymentMethods, detachStripePaymentMethod } from "store/root/payment/stripe/paymentMethods/actions";
import Delete from "./Delete";

const mapDispatchToProps = (dispatch: any, ownProps: any) => {
  return {
    deleteStripePaymentMethod: () => dispatch(detachStripePaymentMethod(ownProps.paymentMethodId)).then(() => dispatch(loadStripePaymentMethods()))
  };
};

export default connect(
  null,
  mapDispatchToProps
)(Delete);
