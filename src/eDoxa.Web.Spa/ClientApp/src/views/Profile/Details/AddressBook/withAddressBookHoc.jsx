import React, { Component } from "react";
import { connect } from "react-redux";
import { loadAddressBook } from "../../../../store/actions/identityActions";

const withAddressBookHoc = WrappedComponent => {
  class AddressBookContainer extends Component {
    componentDidMount() {
      this.props.actions.loadAddressBook();
    }

    render() {
      return <WrappedComponent addressBook={this.props.addressBook} />;
    }
  }

  const mapStateToProps = state => {
    return {
      addressBook: state.user.addressBook
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        loadAddressBook: () => dispatch(loadAddressBook())
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(AddressBookContainer);
};

export default withAddressBookHoc;
