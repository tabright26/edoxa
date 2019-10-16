import { connect } from "react-redux";
import { RootState } from "store/root/types";
import { loadStripePaymentMethods, updateStripePaymentMethod } from "store/root/payment/stripe/paymentMethods/actions";
import Update from "./Update";

const mapStateToProps = (state: RootState, ownProps: any) => {
  const {
    data: { data, hasMore },
    error,
    loading
  } = state.payment.stripe.paymentMethods;
  const paymentMethod = data.find(paymentMethod => paymentMethod.id === ownProps.paymentMethodId);
  return {
    initialValues: paymentMethod
  };
};

const mapDispatchToProps = (dispatch: any, ownProps: any) => {
  return {
    updateStripePaymentMethod: (data: any) => dispatch(updateStripePaymentMethod(ownProps.paymentMethodId, data.card.exp_month, data.card.exp_year)).then(() => dispatch(loadStripePaymentMethods()))
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Update);
