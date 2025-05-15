import React, { useState } from "react";

const Login = () => {
  const [campus, setCampus] = useState("");

  const campuses = [
    { value: "HoaLac", label: "Hòa Lạc" },
    { value: "HCM", label: "Hồ Chí Minh" },
    { value: "Danang", label: "Đà Nẵng" },
    { value: "CanTho", label: "Cần Thơ" },
    { value: "QuyNhon", label: "Quy Nhơn" },
  ];

  const handleCampusChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setCampus(e.target.value);
  };

  const handleGoogleLogin = () => {
    if (!campus) {
      alert("Please select a campus before logging in.");
      return;
    }
    const selectedLabel = campuses.find(c => c.value === campus)?.label || "unknown";
    alert(`Login in with Google for ${selectedLabel} campus`);
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-white">
      <div className="w-full max-w-sm p-8 border rounded-lg shadow-md text-center">
        <div className="mx-auto w-16 h-16 rounded-full bg-gray-300 mb-4" />

        <h1 className="text-xl font-semibold mb-1">FPT University</h1>
        <p className="text-sm text-gray-500 mb-4">Login to continue</p>

        <select
          value={campus}
          onChange={handleCampusChange}
          className="w-full border border-gray-300 rounded px-3 py-2 mb-4 focus:outline-none focus:ring focus:ring-orange-300"
        >
          <option value="" disabled={campus !== ""}>
            Select Campus
          </option>
          {campuses.map((c) => (
            <option key={c.value} value={c.value}>
              {c.label}
            </option>
          ))}
        </select>

        <button
          onClick={handleGoogleLogin}
          className={`w-full flex items-center justify-center gap-2 border rounded px-4 py-2 text-sm font-medium ${
            campus ? "hover:bg-gray-100" : "bg-gray-100 cursor-not-allowed text-gray-400"
          }`}
        >
          <img
            src="https://www.google.com/favicon.ico"
            alt="Google"
            className="w-5 h-5"
          />
          Login with Google
        </button>
      </div>
    </div>
  );
};

export default Login;