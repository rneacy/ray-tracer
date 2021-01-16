#pragma once

#include <cmath>
#include <iostream>

class Vector3 {
public:
	// init of member values, sexy
	Vector3() : vec{ 0, 0, 0 } {}
	Vector3(double x, double y, double z) : vec{ x, y, z } {}

	// returns for vec member
	double x() const {
		return vec[0];
	}

	double y() const {
		return vec[1];
	}

	double z() const {
		return vec[2];
	}

	// Operator Overloads
	// ---------

	// Negation
	Vector3 operator -() const {
		return Vector3(-vec[0], -vec[1], -vec[2]);
	}

	// Array indexing
	double operator [](int i) const {
		return vec[i];
	}
	double& operator [](int i) {
		return vec[i];
	}

	// Arithmetic operator overloads v1

	Vector3& operator +=(const Vector3& other) {
		vec[0] += other.vec[0];
		vec[1] += other.vec[1];
		vec[2] += other.vec[2];
		return *this;
	}

	Vector3& operator *=(const double m) {
		vec[0] *= m;
		vec[1] *= m;
		vec[2] *= m;
		return *this;
	}

	Vector3& operator /=(const double d) {
		vec[0] /= d;
		vec[1] /= d;
		vec[2] /= d;
		return *this;
	}

	double Length() const {
		return std::sqrt(Len_Square());
	}

	double Len_Square() const {
		return vec[0] * vec[0] + vec[1] * vec[1] + vec[2] * vec[2];
	}
private:
	double vec[3];
};

// Aliases (they're all v3s, just with a nicer user friendly name)

using Point3 = Vector3;
using Colour = Vector3;

// << overload for nice cout
inline std::ostream& operator <<(std::ostream& out, const Vector3& vec) {
	return out << vec.x() << " " << vec.y() << " " << vec.z();
}

// Arithmetic operator overloads v2 (the mathsy stuff)

inline Vector3 operator +(const Vector3& first, const Vector3& second) {
	return Vector3(first[0] + second[0], first[1] + second[1], first[2] + second[2]);
}

inline Vector3 operator -(const Vector3& first, const Vector3& second) {
	return Vector3(first[0] - second[0], first[1] - second[1], first[2] - second[2]);
}

inline Vector3 operator *(const Vector3& first, const Vector3& second) {
	return Vector3(first[0] * second[0], first[1] * second[1], first[2] * second[2]);
}

inline Vector3 operator *(double m, const Vector3& vec) {
	return Vector3(vec[0] * m, vec[1] * m, vec[2] * m);
}

inline Vector3 operator *(const Vector3& vec , double m) {
	return m * vec; // already defined above, just do it the same way.
}

inline Vector3 operator /(Vector3 vec, double d) {
	return (1 / d) * vec;
}

inline double Dot(const Vector3& first, const Vector3& second) {
	return first[0] * second[0] + first[1] * second[1] + first[2] * second[2];
}

inline Vector3 Cross(const Vector3& first, const Vector3& second) {
	return Vector3(
		first[0] * second[2] - first[2] * second[1],
		first[2] * second[0] - first[0] * second[2],
		first[0] * second[1] - first[1] * second[0]
	);
}

inline Vector3 Norm(Vector3 vec) {
	return vec / vec.Length();
}