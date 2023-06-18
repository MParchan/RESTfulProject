function Layout(props) {
  return (
    <div style={{ paddingLeft: "20%", paddingRight: "20%" }}>
      {props.children}
    </div>
  );
}

export default Layout;
