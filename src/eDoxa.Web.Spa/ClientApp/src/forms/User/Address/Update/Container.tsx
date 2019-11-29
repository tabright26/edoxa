import { connect } from "react-redux";
import { RootState } from "store/types";
import Update from "./Update";
import { AddressId } from "types";

interface OwnProps {
  addressId: AddressId;
}

const mapStateToProps = (state: RootState, ownProps: OwnProps) => {
  const { data } = state.root.user.addressBook;
  return {
    initialValues: data.find(address => address.id === ownProps.addressId)
  };
};

export default connect(mapStateToProps)(Update);
