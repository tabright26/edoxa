import { connect } from "react-redux";
import { RootState } from "store/types";
import Update from "./Update";
import { AddressId } from "types";
import { updateUserAddress } from "store/root/user/addressBook/actions";

interface OwnProps {
  addressId: AddressId;
}

const mapStateToProps = (state: RootState, ownProps: OwnProps) => {
  const { data } = state.root.user.addressBook;
  return {
    initialValues: data.find(address => address.id === ownProps.addressId)
  };
};

const mapDispatchToProps = (dispatch: any, ownProps: OwnProps) => {
  return {
    updateUserAddress: (data: any) =>
      dispatch(updateUserAddress(ownProps.addressId, data))
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(Update);
